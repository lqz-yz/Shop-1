using API.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using COMMON;
using IBLL;
using BLL;
using JWT.Algorithms;
using JWT;
using JWT.Serializers;
using MODEL;

namespace API.Controllers
{
    public class AuthController : ApiController
    {
        public IMemberBLL Bll { get { return new MemberBLL(); } }

        [Route("api/auth/getToken")]
        [HttpPost]
        public ResponsMessage<string> GetToken(MemberVModel memberVModel)
        {
            try
            {
                string openIdUrl = $"https://api.weixin.qq.com/sns/jscode2session?appid={ConfigurationManager.AppSettings["AppID"]}&secret={ConfigurationManager.AppSettings["AppSecret"]}&js_code={memberVModel.Code}&grant_type=authorization_code";
                //服务器发起http请求（爬虫）
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(openIdUrl);
                request.Method = "GET";
                //获取返回头
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //获取响应流
                Stream Responsestream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(Responsestream, System.Text.Encoding.UTF8);
                string retString = streamReader.ReadToEnd();
                streamReader.Close();
                Responsestream.Close();
                string openId = JsonConvert.DeserializeObject<dynamic>(retString).openid.ToString();
                //判断数据库中是否存在该用户（根据openId）
                var member = Bll.Search(x => x.OpenId == openId).First();
                if (member == null)
                {
                    memberVModel.UserInfo.OpenId = openId;
                    Bll.Add(memberVModel.UserInfo);
                    member = memberVModel.UserInfo;
                }

                //生成token
                var payload = new Dictionary<string, object>
                {
                    { "UserName", member.NickName+Guid.NewGuid().ToString("N")}
                };
                IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
                IJsonSerializer serializer = new JsonNetSerializer();
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
                var token= encoder.Encode(payload, ConfigurationManager.AppSettings["JwtSecret"]);
                //将token存入redis
                RedisHelper.Set(token, member.ID, DateTime.Now.AddDays(7) - DateTime.Now);

                return new ResponsMessage<string>()
                {
                    Code = 200,
                    Data = token
                };
            }
            catch (Exception ex)
            {
                return new ResponsMessage<string>()
                {
                    Code = 401,
                    Message = ex.Message
                };
            }        
        }

        //[AuthFilter]
        //[Route("api/auth/test")]
        //[HttpGet]
        //public string AuthTest() {
        //    return "Auth OK";
        //}
    }
}
