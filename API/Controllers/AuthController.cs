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
using StackExchange.Redis;
using IBLL;
using BLL;

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
                var member = Bll.Search(x => x.OpenId == openId);
                if (member.Count == 0)
                {
                    memberVModel.UserInfo.OpenId = openId;
                    Bll.Add(memberVModel.UserInfo);
                }
                //生成token
                var token= COMMON.TokenHelper.GenToken(member[0]);


                //string salt = "#%&$#&)%";
                //string time = DateTime.Now.ToString("yyyyMMddHHmmssfffff");
                //string guid = Guid.NewGuid().ToString("N");
                //string random = new Random().Next(10000, 99999).ToString();
                //string str = salt + time + guid + random;
                //string token = Md5Helper.Md5(Md5Helper.Md5(str));
                //将token存入redis
                var conn = ConnectionMultiplexer.Connect("192.168.137.111:6379,password=123456");
                //1.指定操作的数据库
                var db = conn.GetDatabase(0);
                //设置token的有效期为7天,openId也可以是数据库的id，唯一标识一个用户就可以
                db.StringSet(token, openId, DateTime.Now.AddDays(7) - DateTime.Now);

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
                    Code = 500,
                    Message = ex.Message
                };
            }

        }
    }
}
