using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;
using WXSDK.Common;
using WXSDK.Models;
using System.Globalization;
using Newtonsoft.Json;
using System.Net;
using System.Media;

namespace WXSDK
{
    /// <summary>
    /// 微信网页版SDK
    /// </summary>
    public class WXWebApiHelper
    {
        CommonHelper _CommonHelper = new CommonHelper();

        #region 配置常量
        //微信开发者ID(微信提供)
        const string WXUUID = "wx782c26e4c19acffb";
        //微信二维码图片ID(GET)
        const string WXIMGIDURL = "https://login.weixin.qq.com/jslogin";
        //微信二维码图片(GET)
        const string WXIMGURL = "https://login.weixin.qq.com/qrcode/{0}?t=webwx";
        //微信等待登录(GET)
        const string WXLOGINURL = "https://login.weixin.qq.com/cgi-bin/mmwebwx-bin/login?tip={0}&uuid={1}&_={2}";
        //微信初始化(POST)
        const string WXUSERINFOURL = "https://wx2.qq.com/cgi-bin/mmwebwx-bin/webwxinit?r={0}&lang=zh_CN&pass_ticket={1}";
        //开启微信状态通知(POST)
        const string WXSTATUSNOTIFYURL = "https://wx2.qq.com/cgi-bin/mmwebwx-bin/webwxstatusnotify?lang=zh_CN&pass_ticket={0}";
        //微信联系人信息(POST)
        const string WXGETCONTACTURLURL = "https://wx2.qq.com/cgi-bin/mmwebwx-bin/webwxgetcontact?pass_ticket={0}&skey={1}&r={2}";
        //微信联系人详细信息(POST)
        const string WXBATCHGETCONTACTURL = "https://wx2.qq.com/cgi-bin/mmwebwx-bin/webwxbatchgetcontact?type=ex&r={0}&pass_ticket={1}";
        //保持与服务器的信息同步(GET)
        const string WXSYNCCHECKURL = "https://webpush.wx2.qq.com/cgi-bin/mmwebwx-bin/synccheck";
        //获取服务器当前产生的消息(POST)
        const string WXSYNCURL = "https://wx2.qq.com/cgi-bin/mmwebwx-bin/webwxsync?sid={0}&skey={1}&lang=zh_CN&pass_ticket={2}";
        //微信发送文本消息(POST)
        const string WXSENDMSGURL = "https://wx2.qq.com/cgi-bin/mmwebwx-bin/webwxsendmsg?sid={0}&lang=zh_CN&pass_ticket={1}";
        //微信发送图片消息(POST)
        const string WXSENDMSGIMGURL = "https://wx2.qq.com/cgi-bin/mmwebwx-bin/webwxsendmsgimg?fun=async&f=json&lang=zh_CN";
        //微信发送文件消息(POST)
        const string WXSENDAPPMSGURL = "https://wx2.qq.com/cgi-bin/mmwebwx-bin/webwxsendappmsg?fun=async&f=json";
        //微信给好友添加备注，置顶聊天，取消聊天(POST)
        const string WXOPLOGURL = "https://wx2.qq.com/cgi-bin/mmwebwx-bin/webwxoplog";
        //微信修改群聊名称(POST)
        const string WXUPDATECHATROOMURL = "https://wx2.qq.com/cgi-bin/mmwebwx-bin/webwxupdatechatroom?fun=modtopic";
        //微信消息撤回(POST)
        const string WXREVOKEMSGURL = "https://wx2.qq.com/cgi-bin/mmwebwx-bin/webwxrevokemsg";
        //微信添加好友,微信号关注,验证通过(POST)
        const string WXVERIFYUSERURL = "https://wx2.qq.com/cgi-bin/mmwebwx-bin/webwxverifyuser?r={0}";
        //微信退出(POST)
        const string WXLOGINOUTURL = "https://wx2.qq.com/cgi-bin/mmwebwx-bin/webwxlogout?redirect=1&type=1&skey={0}";

        //微信文件上传(POST)
        const string WXUPLOADMEDIAURL = "https://file.wx2.qq.com/cgi-bin/mmwebwx-bin/webwxuploadmedia?f=json";
        //微信联系人头像图片(GET)
        const string WXGETICONURL = "https://wx2.qq.com/cgi-bin/mmwebwx-bin/webwxgeticon?seq=0&username={0}&skey={1}";
        //微信群头像图片(GET)
        const string WXGETHEADIMGURL = "https://wx2.qq.com/cgi-bin/mmwebwx-bin/webwxgetheadimg?seq=0&username={0}&skey={1}";
        //微信消息图片(GET)
        const string WXGETMSGIMGURL = "https://wx2.qq.com/cgi-bin/mmwebwx-bin/webwxgetmsgimg?MsgID={0}&type=slave&skey={1}";
        //微信语音消息(GET)
        const string WXGETVOICEURL = "https://wx2.qq.com/cgi-bin/mmwebwx-bin/webwxgetvoice?msgid={0}&skey={1}";
        //微信视频消息(GET)
        const string WXGETVIDEOURL = "https://wx2.qq.com/cgi-bin/mmwebwx-bin/webwxgetvideo?fun=download&msgid={0}&skey={1}";
        //微信其他文件(GET)
        const string WXGETMEDIAURL = "https://file.wx2.qq.com/cgi-bin/mmwebwx-bin/webwxgetmedia?sender={0}&mediaid={1}&encryfilename={2}&fromuser={3}&pass_ticket={4}&webwx_data_ticket={5}";
        #endregion

        #region 1 GetLoginQRCode 获取登录二维码图片
        /// <summary>
        /// 获取登录二维码图片
        /// </summary>
        /// <returns></returns>
        public ResultEntity<OutGetLoginQRCode> GetLoginQRCode()
        {
            var result = new ResultEntity<OutGetLoginQRCode>();
            var resultmodel = new OutGetLoginQRCode();
            ///构造请求参数表和基础地址
            Dictionary<string, string> _dic = new Dictionary<string, string>();
            _dic.Add("appid", WXUUID);
            _dic.Add("fun", "new");
            _dic.Add("lang", "zh_CN");
            _dic.Add("_", _CommonHelper.GetUTCMillTime());
            //通过返回的Html内容获取imgID
            CookieCollection _CookieCollection = new CookieCollection();
            string html = _CommonHelper.HttpGet(WXIMGIDURL, _dic, ref _CookieCollection).Html;
            Regex reg = new Regex("window.QRLogin.code = (\\d+); window.QRLogin.uuid = \"(\\S+?)\"");
            var resultreg = reg.Match(html).Groups;
            int code = 400;
            string imageID = "";
            if (resultreg.Count == 3)
            {
                code = resultreg[1].ToInt();
                imageID = resultreg[2].ToString();
            }
            ///通过imgID获取二维码图片
            Image img = null;
            var qruuid = "";
            if (code == 200 && string.IsNullOrEmpty(imageID) == false)
            {
                string str = WXIMGURL;
                str = string.Format(str, imageID);
                img = _CommonHelper.GetImage(str);
                qruuid = imageID;

                resultmodel.img = img;
                resultmodel.qruuid = qruuid;
                resultmodel._CookieCollection = _CookieCollection;
                result.ResultCode = 200;
                result.ResultObject = resultmodel;
            }
            else
            {
                result.ResultCode = code;
            }
            return result;
        }
        #endregion

        #region 2 WaitForLogin 检查二维码扫描状态
        /// <summary>
        /// 检查二维码扫描状态（轮询调用）
        /// </summary>
        /// <param name="qruuid">获取当前所有请求的基础字符串，域名部分.带着前缀的(步骤1获得)</param>
        /// <param name="tip">201状态码需传tip:0</param>
        /// <param name="_CookieCollection">步骤1得到的CookieCollection</param>
        /// <returns></returns>
        public ResultEntity<OutWaitForLoginEntity> WaitForLogin(string qruuid, CookieCollection _CookieCollection, int tip = 1)
        {
            var result = new ResultEntity<OutWaitForLoginEntity>();
            var resultmodel = new OutWaitForLoginEntity();

            string url = string.Format(WXLOGINURL, tip, qruuid, _CommonHelper.GetUTCMillTime());
            string html = _CommonHelper.HttpGet(url, null, ref _CookieCollection).Html;
            Regex reg = new Regex("window.code=(\\d+);");
            var resultreg = reg.Match(html).Groups;
            string codeStr = resultreg[1].ToString();
            switch (codeStr)
            {
                case "201"://已扫描，未确认
                    tip = 0;
                    result.ResultCode = 201;
                    result.ResultMsg = "已扫描，未确认";
                    break;
                case "200"://登录成功
                    Regex reg1 = new Regex("window.redirect_uri=\"(\\S+?)\";");
                    var resultreg1 = reg1.Match(html).Groups;
                    var redirect_uri = resultreg1[1] + "&fun=new";
                    resultmodel.redirect_uri = redirect_uri;
                    resultmodel._CookieCollection = _CookieCollection;
                    result.ResultCode = 200;
                    result.ResultObject = resultmodel;
                    break;
                case "408":
                    result.ResultCode = 408;
                    result.ResultMsg = "登录超时";
                    break;
                default:
                    result.ResultCode = 500;
                    result.ResultMsg = "其他状态";
                    break;
            }
            return result;
        }
        #endregion

        #region 3 Login 用户登录
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="redirect_uri">登录成功后获取到的重定向地址，使用一次(步骤2获得)</param>
        /// <param name="DeviceID">设备ID(由e开头与数字形成的随机数)(e123456789)</param>
        /// <param name="_CookieCollection">步骤2得到的CookieCollection</param>
        /// <returns></returns>
        public ResultEntity<OutLoginEntity> Login(string redirect_uri, string DeviceID, CookieCollection _CookieCollection)
        {
            var result = new ResultEntity<OutLoginEntity>();
            var resultmodel = new OutLoginEntity();

            if (redirect_uri.Length > 4)
            {
                //获取登录成功后反馈的重定向地址的内容
                var htmlresult = _CommonHelper.HttpGet(redirect_uri, null, ref  _CookieCollection);
                var htmlstr = htmlresult.Html;
                byte[] byteArray = Encoding.UTF8.GetBytes(htmlstr);
                MemoryStream stream = new MemoryStream(byteArray);

                XmlReaderSettings settings = new XmlReaderSettings();
                settings.IgnoreComments = true;
                using (XmlReader reader = XmlReader.Create(stream, settings))
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(reader);
                    XmlNode xn = xmlDoc.SelectSingleNode("error");
                    var Pass_ticket = "";
                    var Skey = "";
                    var Sid = "";
                    var Uin = "";
                    foreach (XmlNode noneode in xn.ChildNodes)
                    {
                        if (noneode.Name == "skey")
                        {
                            Skey = noneode.ChildNodes[0].InnerText;
                        }
                        else if (noneode.Name == "wxsid")
                        {
                            Sid = noneode.ChildNodes[0].InnerText;
                        }
                        else if (noneode.Name == "wxuin")
                        {
                            Uin = noneode.ChildNodes[0].InnerText;
                        }
                        else if (noneode.Name == "pass_ticket")
                        {
                            Pass_ticket = noneode.ChildNodes[0].InnerText;
                        }
                    }
                    BaseRequest BaseRequest = new BaseRequest();
                    BaseRequest.Uin = Uin;
                    BaseRequest.Sid = Sid;
                    BaseRequest.Skey = Skey;
                    BaseRequest.DeviceID = DeviceID;
                    resultmodel.Pass_ticket = Pass_ticket;
                    resultmodel.Skey = Skey;
                    resultmodel.Sid = Sid;
                    resultmodel.Uin = Uin;
                    resultmodel.BaseRequest = BaseRequest;
                    resultmodel._CookieCollection = _CookieCollection;
                    result.ResultObject = resultmodel;
                    result.ResultCode = 200;
                }
            }
            return result;
        }
        #endregion

        #region 4 Init 微信初始化
        /// <summary>
        /// 微信初始化
        /// </summary>
        /// <param name="Pass_ticket">微信认证标志(步骤3获得)</param>
        /// <param name="BaseRequest">所需post字段(步骤3获得)</param>
        /// <param name="_CookieCollection">步骤3得到的CookieCollection</param>
        /// <returns></returns>
        public ResultEntity<OutInitEntity> Init(string Pass_ticket, BaseRequest BaseRequest, CookieCollection _CookieCollection)
        {
            var result = new ResultEntity<OutInitEntity>();
            var resultmodel = new OutInitEntity();
            string initUrl = string.Format(WXUSERINFOURL, _CommonHelper.GetUTCMillTime(10000000), Pass_ticket);
            Dictionary<string, object> _dic = new Dictionary<string, object>();
            _dic.Add("BaseRequest", BaseRequest);
            string htmlStr = _CommonHelper.HttpPost(initUrl, _dic, ref _CookieCollection).Html;
            WXInitRoot wxInitObj = Newtonsoft.Json.JsonConvert.DeserializeObject<WXInitRoot>(htmlStr);

            if (wxInitObj != null && wxInitObj.BaseResponse != null)
            {
                resultmodel.ChatGroupLstEntity = new List<MemberObjEntity>();
                //当前账号
                if (wxInitObj.User != null)
                {
                    //获取最近的群聊信息(已包含群成员信息)
                    if (wxInitObj.ContactList.Count > 0)
                    {
                        wxInitObj.ContactList.ForEach(j =>
                        {
                            if (j.UserName.IndexOf("@@") != -1)
                            {
                                j.HeadImg = GetheadImg(j.UserName, BaseRequest.Skey, _CookieCollection);
                                resultmodel.ChatGroupLstEntity.Add(j);
                            }
                        });
                    }
                    //获取头像图片
                    wxInitObj.User.HeadImg = GeticonImg(wxInitObj.User.UserName, BaseRequest.Skey, _CookieCollection);
                    resultmodel._CookieCollection = _CookieCollection;
                    resultmodel.UserEntity = wxInitObj.User;
                    resultmodel.SyncKeyEntity = wxInitObj.SyncKey;
                    result.ResultObject = resultmodel;
                    result.ResultCode = 200;
                }
            }
            return result;
        }
        #endregion

        #region 5 StatusNotify 开启微信状态通知(通知消息已读)
        /// <summary>
        /// 开启微信状态通知
        /// </summary>
        /// <param name="Pass_ticket">微信认证标志(步骤3获得)</param>
        /// <param name="FromUserName">用户ID</param>
        /// <param name="ToUserName">发送用户ID(开启微信状态通知为用户自身ID)</param>
        /// <param name="Code">开启微信状态通知值为3，通知消息已读值为1</param>
        /// <param name="BaseRequest">所需post字段(步骤3获得)</param>
        /// <param name="_CookieCollection">步骤4得到的CookieCollection</param>
        /// <returns></returns>
        public ResultEntity<OutStatusNotifyEntity> StatusNotify(string Pass_ticket, string FromUserName, string ToUserName, int Code, BaseRequest BaseRequest, CookieCollection _CookieCollection)
        {
            var result = new ResultEntity<OutStatusNotifyEntity>();
            var resultmodel = new OutStatusNotifyEntity();
            string status_notify_Url = string.Format(WXSTATUSNOTIFYURL, Pass_ticket);

            Dictionary<string, object> _dic = new Dictionary<string, object>();
            _dic.Add("BaseRequest", BaseRequest);
            _dic.Add("Code", Code);
            _dic.Add("FromUserName", FromUserName);
            _dic.Add("ToUserName", ToUserName);
            _dic.Add("ClientMsgId", _CommonHelper.GetUTCMillTime(10000000));

            string htmlStr = _CommonHelper.HttpPost(status_notify_Url, _dic, ref _CookieCollection).Html;

            WXInitRoot curObj = Newtonsoft.Json.JsonConvert.DeserializeObject<WXInitRoot>(htmlStr);
            if (curObj != null && curObj.BaseResponse != null)
            {
                resultmodel.status = curObj.BaseResponse.Ret.Equals(0);
                if (resultmodel.status)
                {
                    resultmodel._CookieCollection = _CookieCollection;
                    result.ResultObject = resultmodel;
                    result.ResultCode = 200;
                }
            }

            return result;
        }
        #endregion

        #region 6 GetContact 获取用户联系人(普通成员，公众号，群聊，其他)（需要步骤12345累计获得的CookieCollection）
        /// <summary>
        /// 获取用户联系人(普通成员，公众号，群聊，其他)
        /// </summary>
        /// <param name="Pass_ticket">微信认证标志(步骤3获得)</param>
        /// <param name="Skey">微信认证标志(步骤3获得)</param>
        /// <param name="_CookieCollection">从前面12345步骤获得的CookieCollection作为请求参数</param>
        /// <returns></returns>
        public ResultEntity<OutGetContactEntity> GetContact(string Pass_ticket, string Skey, CookieCollection _CookieCollection)
        {
            var result = new ResultEntity<OutGetContactEntity>();
            var resultmodel = new OutGetContactEntity();
            string get_contact_Url = string.Format(WXGETCONTACTURLURL, Pass_ticket, Skey, _CommonHelper.GetUTCMillTime(10000000));
            Dictionary<string, object> _dic = new Dictionary<string, object>();
            string htmlStr = _CommonHelper.HttpPost(get_contact_Url, _dic, ref _CookieCollection).Html;
            WebWXMemberObject wxMemberObj = Newtonsoft.Json.JsonConvert.DeserializeObject<WebWXMemberObject>(htmlStr);

            if (wxMemberObj != null && wxMemberObj.BaseResponse != null)
            {
                wxMemberObj.SortMembers();

                WebWXMemberObject inf = wxMemberObj;

                #region 群聊(不包含群成员信息)
                resultmodel.ChatGroupLstEntity = new List<MemberObjEntity>();
                if (inf.GroupMember.Count > 0)
                {
                    for (int i = 0; i < inf.GroupMember.Count; i++)
                    {
                        var _ChatGroupEntity = new MemberObjEntity();
                        _ChatGroupEntity.Alias = inf.GroupMember[i].Alias;
                        _ChatGroupEntity.AppAccountFlag = inf.GroupMember[i].AppAccountFlag;
                        _ChatGroupEntity.AttrStatus = inf.GroupMember[i].AttrStatus;
                        _ChatGroupEntity.HideInputBarFlag = inf.GroupMember[i].HideInputBarFlag;
                        _ChatGroupEntity.ChatRoomId = inf.GroupMember[i].ChatRoomId;
                        _ChatGroupEntity.IsOwner = inf.GroupMember[i].IsOwner;
                        _ChatGroupEntity.City = inf.GroupMember[i].City;
                        _ChatGroupEntity.KeyWord = inf.GroupMember[i].KeyWord;
                        _ChatGroupEntity.ContactFlag = inf.GroupMember[i].ContactFlag;
                        _ChatGroupEntity.MemberCount = inf.GroupMember[i].MemberCount;
                        _ChatGroupEntity.DisplayName = inf.GroupMember[i].DisplayName;
                        _ChatGroupEntity.NickName = inf.GroupMember[i].NickName;
                        _ChatGroupEntity.EncryChatRoomId = inf.GroupMember[i].EncryChatRoomId;
                        _ChatGroupEntity.OwnerUin = inf.GroupMember[i].OwnerUin;
                        _ChatGroupEntity.HeadImgUrl = inf.GroupMember[i].HeadImgUrl;
                        _ChatGroupEntity.PYInitial = inf.GroupMember[i].PYInitial;
                        _ChatGroupEntity.PYQuanPin = inf.GroupMember[i].PYQuanPin;
                        _ChatGroupEntity.RemarkPYInitial = inf.GroupMember[i].RemarkPYInitial;
                        _ChatGroupEntity.RemarkName = inf.GroupMember[i].RemarkName;
                        _ChatGroupEntity.RemarkPYQuanPin = inf.GroupMember[i].RemarkPYQuanPin;
                        _ChatGroupEntity.Sex = inf.GroupMember[i].Sex;
                        _ChatGroupEntity.SnsFlag = inf.GroupMember[i].SnsFlag;
                        _ChatGroupEntity.Signature = inf.GroupMember[i].Signature;
                        _ChatGroupEntity.StarFriend = inf.GroupMember[i].StarFriend;
                        _ChatGroupEntity.Statues = inf.GroupMember[i].Statues;
                        _ChatGroupEntity.Uin = inf.GroupMember[i].Uin;
                        _ChatGroupEntity.UniFriend = inf.GroupMember[i].UniFriend;
                        _ChatGroupEntity.UserName = inf.GroupMember[i].UserName;
                        _ChatGroupEntity.VerifyFlag = inf.GroupMember[i].VerifyFlag;
                        //群聊头像
                        _ChatGroupEntity.HeadImg = GetheadImg(_ChatGroupEntity.UserName, Skey, _CookieCollection);
                        resultmodel.ChatGroupLstEntity.Add(_ChatGroupEntity);
                    }
                }
                #endregion

                #region 普通成员
                resultmodel.MemUserLstEntity = new List<MemberObjEntity>();
                if (inf.NormalMember.Count > 0)
                {
                    for (int i = 0; i < inf.NormalMember.Count; i++)
                    {
                        var _MemUserEntity = new MemberObjEntity();
                        _MemUserEntity.Alias = inf.NormalMember[i].Alias;
                        _MemUserEntity.AppAccountFlag = inf.NormalMember[i].AppAccountFlag;
                        _MemUserEntity.AttrStatus = inf.NormalMember[i].AttrStatus;
                        _MemUserEntity.HideInputBarFlag = inf.NormalMember[i].HideInputBarFlag;
                        _MemUserEntity.ChatRoomId = inf.NormalMember[i].ChatRoomId;
                        _MemUserEntity.IsOwner = inf.NormalMember[i].IsOwner;
                        _MemUserEntity.City = inf.NormalMember[i].City;
                        _MemUserEntity.KeyWord = inf.NormalMember[i].KeyWord;
                        _MemUserEntity.ContactFlag = inf.NormalMember[i].ContactFlag;
                        _MemUserEntity.MemberCount = inf.NormalMember[i].MemberCount;
                        _MemUserEntity.DisplayName = inf.NormalMember[i].DisplayName;
                        _MemUserEntity.NickName = inf.NormalMember[i].NickName;
                        _MemUserEntity.EncryChatRoomId = inf.NormalMember[i].EncryChatRoomId;
                        _MemUserEntity.OwnerUin = inf.NormalMember[i].OwnerUin;
                        _MemUserEntity.HeadImgUrl = inf.NormalMember[i].HeadImgUrl;
                        _MemUserEntity.PYInitial = inf.NormalMember[i].PYInitial;
                        _MemUserEntity.PYQuanPin = inf.NormalMember[i].PYQuanPin;
                        _MemUserEntity.RemarkPYInitial = inf.NormalMember[i].RemarkPYInitial;
                        _MemUserEntity.RemarkName = inf.NormalMember[i].RemarkName;
                        _MemUserEntity.RemarkPYQuanPin = inf.NormalMember[i].RemarkPYQuanPin;
                        _MemUserEntity.Sex = inf.NormalMember[i].Sex;
                        _MemUserEntity.SnsFlag = inf.NormalMember[i].SnsFlag;
                        _MemUserEntity.Signature = inf.NormalMember[i].Signature;
                        _MemUserEntity.StarFriend = inf.NormalMember[i].StarFriend;
                        _MemUserEntity.Statues = inf.NormalMember[i].Statues;
                        _MemUserEntity.Uin = inf.NormalMember[i].Uin;
                        _MemUserEntity.UniFriend = inf.NormalMember[i].UniFriend;
                        _MemUserEntity.UserName = inf.NormalMember[i].UserName;
                        _MemUserEntity.VerifyFlag = inf.NormalMember[i].VerifyFlag;
                        //用户头像
                        _MemUserEntity.HeadImg = GeticonImg(_MemUserEntity.UserName, Skey, _CookieCollection);
                        resultmodel.MemUserLstEntity.Add(_MemUserEntity);
                    }
                }
                #endregion

                #region 公众号
                resultmodel.MemberGroupLstEntity = new List<MemberObjEntity>();
                if (inf.PublicMember.Count > 0)
                {
                    for (int i = 0; i < inf.PublicMember.Count; i++)
                    {
                        var _MemberGroupEntity = new MemberObjEntity();
                        _MemberGroupEntity.Alias = inf.PublicMember[i].Alias;
                        _MemberGroupEntity.AppAccountFlag = inf.PublicMember[i].AppAccountFlag;
                        _MemberGroupEntity.AttrStatus = inf.PublicMember[i].AttrStatus;
                        _MemberGroupEntity.HideInputBarFlag = inf.PublicMember[i].HideInputBarFlag;
                        _MemberGroupEntity.ChatRoomId = inf.PublicMember[i].ChatRoomId;
                        _MemberGroupEntity.IsOwner = inf.PublicMember[i].IsOwner;
                        _MemberGroupEntity.City = inf.PublicMember[i].City;
                        _MemberGroupEntity.KeyWord = inf.PublicMember[i].KeyWord;
                        _MemberGroupEntity.ContactFlag = inf.PublicMember[i].ContactFlag;
                        _MemberGroupEntity.MemberCount = inf.PublicMember[i].MemberCount;
                        _MemberGroupEntity.DisplayName = inf.PublicMember[i].DisplayName;
                        _MemberGroupEntity.NickName = inf.PublicMember[i].NickName;
                        _MemberGroupEntity.EncryChatRoomId = inf.PublicMember[i].EncryChatRoomId;
                        _MemberGroupEntity.OwnerUin = inf.PublicMember[i].OwnerUin;
                        _MemberGroupEntity.HeadImgUrl = inf.PublicMember[i].HeadImgUrl;
                        _MemberGroupEntity.PYInitial = inf.PublicMember[i].PYInitial;
                        _MemberGroupEntity.PYQuanPin = inf.PublicMember[i].PYQuanPin;
                        _MemberGroupEntity.RemarkPYInitial = inf.PublicMember[i].RemarkPYInitial;
                        _MemberGroupEntity.RemarkName = inf.PublicMember[i].RemarkName;
                        _MemberGroupEntity.RemarkPYQuanPin = inf.PublicMember[i].RemarkPYQuanPin;
                        _MemberGroupEntity.Sex = inf.PublicMember[i].Sex;
                        _MemberGroupEntity.SnsFlag = inf.PublicMember[i].SnsFlag;
                        _MemberGroupEntity.Signature = inf.PublicMember[i].Signature;
                        _MemberGroupEntity.StarFriend = inf.PublicMember[i].StarFriend;
                        _MemberGroupEntity.Statues = inf.PublicMember[i].Statues;
                        _MemberGroupEntity.Uin = inf.PublicMember[i].Uin;
                        _MemberGroupEntity.UniFriend = inf.PublicMember[i].UniFriend;
                        _MemberGroupEntity.UserName = inf.PublicMember[i].UserName;
                        _MemberGroupEntity.VerifyFlag = inf.PublicMember[i].VerifyFlag;
                        //用户头像
                        _MemberGroupEntity.HeadImg = GeticonImg(_MemberGroupEntity.UserName, Skey, _CookieCollection);
                        resultmodel.MemberGroupLstEntity.Add(_MemberGroupEntity);
                    }
                }
                #endregion

                #region 其他
                resultmodel.OtherMemLstEntity = new List<MemberObjEntity>();
                if (inf.OtherMember.Count > 0)
                {
                    for (int i = 0; i < inf.OtherMember.Count; i++)
                    {
                        var _OtherMemEntity = new MemberObjEntity();
                        _OtherMemEntity.Alias = inf.OtherMember[i].Alias;
                        _OtherMemEntity.AppAccountFlag = inf.OtherMember[i].AppAccountFlag;
                        _OtherMemEntity.AttrStatus = inf.OtherMember[i].AttrStatus;
                        _OtherMemEntity.HideInputBarFlag = inf.OtherMember[i].HideInputBarFlag;
                        _OtherMemEntity.ChatRoomId = inf.OtherMember[i].ChatRoomId;
                        _OtherMemEntity.IsOwner = inf.OtherMember[i].IsOwner;
                        _OtherMemEntity.City = inf.OtherMember[i].City;
                        _OtherMemEntity.KeyWord = inf.OtherMember[i].KeyWord;
                        _OtherMemEntity.ContactFlag = inf.OtherMember[i].ContactFlag;
                        _OtherMemEntity.MemberCount = inf.OtherMember[i].MemberCount;
                        _OtherMemEntity.DisplayName = inf.OtherMember[i].DisplayName;
                        _OtherMemEntity.NickName = inf.OtherMember[i].NickName;
                        _OtherMemEntity.EncryChatRoomId = inf.OtherMember[i].EncryChatRoomId;
                        _OtherMemEntity.OwnerUin = inf.OtherMember[i].OwnerUin;
                        _OtherMemEntity.HeadImgUrl = inf.OtherMember[i].HeadImgUrl;
                        _OtherMemEntity.PYInitial = inf.OtherMember[i].PYInitial;
                        _OtherMemEntity.PYQuanPin = inf.OtherMember[i].PYQuanPin;
                        _OtherMemEntity.RemarkPYInitial = inf.OtherMember[i].RemarkPYInitial;
                        _OtherMemEntity.RemarkName = inf.OtherMember[i].RemarkName;
                        _OtherMemEntity.RemarkPYQuanPin = inf.OtherMember[i].RemarkPYQuanPin;
                        _OtherMemEntity.Sex = inf.OtherMember[i].Sex;
                        _OtherMemEntity.SnsFlag = inf.OtherMember[i].SnsFlag;
                        _OtherMemEntity.Signature = inf.OtherMember[i].Signature;
                        _OtherMemEntity.StarFriend = inf.OtherMember[i].StarFriend;
                        _OtherMemEntity.Statues = inf.OtherMember[i].Statues;
                        _OtherMemEntity.Uin = inf.OtherMember[i].Uin;
                        _OtherMemEntity.UniFriend = inf.OtherMember[i].UniFriend;
                        _OtherMemEntity.UserName = inf.OtherMember[i].UserName;
                        _OtherMemEntity.VerifyFlag = inf.OtherMember[i].VerifyFlag;
                        //用户头像
                        _OtherMemEntity.HeadImg = GeticonImg(_OtherMemEntity.UserName, Skey, _CookieCollection);
                        resultmodel.OtherMemLstEntity.Add(_OtherMemEntity);
                    }
                }
                #endregion

                result.ResultObject = resultmodel;
                result.ResultCode = 200;
            }

            return result;
        }
        #endregion

        #region 7 GetBatchContact 获取联系人详细信息(主要用于获取群成员信息)(需要步骤6数据)
        /// <summary>
        /// 获取群成员和联系人信息(主要用于获取群成员信息)
        /// </summary>
        /// <param name="Pass_ticket">微信认证标志(步骤3获得)</param>
        /// <param name="BaseRequest">所需post字段(步骤3获得)</param>
        /// <param name="MemberObjLstEntity">由步骤6获取到的数据，数据类型(群聊，普通成员，公众号，其他)不同也将会取到不同的详细信息</param>
        /// <param name="_CookieCollection"></param>
        /// <returns></returns>
        public ResultEntity<List<MemberObjEntity>> GetBatchContact(string Pass_ticket, BaseRequest BaseRequest, List<MemberObjEntity> MemberObjLstEntity,CookieCollection _CookieCollection)
        {
            var result = new ResultEntity<List<MemberObjEntity>>();
            var resultmodel = new List<MemberObjEntity>();
            var batch_get_group_members_Url = string.Format(WXBATCHGETCONTACTURL, _CommonHelper.GetUTCMillTime(10000000), Pass_ticket);
            Dictionary<string, object> _dic = new Dictionary<string, object>();
            _dic.Add("BaseRequest", BaseRequest);
            _dic.Add("Count", MemberObjLstEntity.Count);
            List<object> allGroupList = new List<object>();
            foreach (MemberObjEntity oneGroup in MemberObjLstEntity)
            {
                Dictionary<string, object> _dicOneGroup = new Dictionary<string, object>();
                _dicOneGroup.Add("UserName", oneGroup.UserName);
                _dicOneGroup.Add("EncryChatRoomId", "");
                allGroupList.Add(_dicOneGroup);
            }
            _dic.Add("List", allGroupList);
            string htmlStr = _CommonHelper.HttpPost(batch_get_group_members_Url, _dic).Html;
            WXGroupuserList _WXGroupuserList = Newtonsoft.Json.JsonConvert.DeserializeObject<WXGroupuserList>(htmlStr);
            if (_WXGroupuserList != null && _WXGroupuserList.Count >= 0)
            {
                for (int i = 0; i < _WXGroupuserList.Count; i++)
                {
                    Image HeadImg = null;
                    if (_WXGroupuserList.ContactList[i].UserName.IndexOf("@@") != -1)
                    {
                        //群聊头像
                        HeadImg = GetheadImg(_WXGroupuserList.ContactList[i].UserName, BaseRequest.Skey, _CookieCollection);
                    }
                    else
                    {
                        //用户头像
                        HeadImg = GeticonImg(_WXGroupuserList.ContactList[i].UserName, BaseRequest.Skey, _CookieCollection);
                    }
                    _WXGroupuserList.ContactList[i].HeadImg = HeadImg;
                    resultmodel.Add(_WXGroupuserList.ContactList[i]);
                }
                result.ResultCode = 200;
                result.ResultObject = resultmodel;
            }
            return result;
        }
        #endregion


        #region SyncCheck 保持与服务器的信息同步
        /// <summary>
        /// 保持与服务器的信息同步(轮询调用)
        /// </summary>
        /// <param name="_BaseRequest">所需post字段(步骤3获得)</param>
        /// <param name="_SyncKeyEntity">步骤4微信初始化获得</param>
        /// <param name="_CookieCollection"></param>
        /// <returns></returns>
        public ResultEntity<int> SyncCheck(BaseRequest _BaseRequest, SyncKeyEntity _SyncKeyEntity, CookieCollection _CookieCollection)
        {
            var result = new ResultEntity<int>();
            int[] rs = new int[] { -1, -1 };
            Dictionary<string, string> _parmDic = new Dictionary<string, string>();
            _parmDic.Add("r", _CommonHelper.GetUTCMillTime(10000000));
            _parmDic.Add("sid", _BaseRequest.Sid);
            _parmDic.Add("uin", _BaseRequest.Uin);
            _parmDic.Add("skey", _BaseRequest.Skey);
            _parmDic.Add("deviceid", _BaseRequest.DeviceID);
            string synckey = "";
            if (_SyncKeyEntity != null && _SyncKeyEntity.List.Count > 0)
            {
                foreach (var oneitem in _SyncKeyEntity.List)
                {
                    if (string.IsNullOrEmpty(synckey))
                    {
                        synckey += oneitem.Key.ToString() + "_" + oneitem.Val.ToString();
                    }
                    else
                    {
                        synckey += "|" + oneitem.Key.ToString() + "_" + oneitem.Val.ToString();
                    }
                }
            }
            _parmDic.Add("synckey", synckey);
            _parmDic.Add("_", _CommonHelper.GetUTCMillTime(10000000));
            string url = WXSYNCCHECKURL;

            string htmlstr = _CommonHelper.HttpGet(url, _parmDic, ref _CookieCollection).Html;
            Regex reg = new Regex("window.synccheck={retcode:\"(\\d+)\",selector:\"(\\d+)\"}");
            var resultreg = reg.Match(htmlstr).Groups;
            if (resultreg.Count == 3)
            {
                rs[0] = resultreg[1].ToInt();
                rs[1] = resultreg[2].ToInt();
                if (rs[0] == 1100)// 从微信客户端上登出
                {
                    result.ResultMsg = "从微信客户端上登出";
                    result.ResultCode = 500;
                    result.ResultObject = 1100;
                }
                else if (rs[0] == 1101)// 从其它设备上登了网页微信
                {
                    result.ResultMsg = "从其它设备上登了网页微信";
                    result.ResultCode = 500;
                    result.ResultObject = 1101;
                }
                else if (rs[0] == 1102)// 从其它设备上登了网页微信
                {
                    result.ResultMsg = "登陆的信息出现错误了,需要重新登陆一次了";
                    result.ResultCode = 500;
                    result.ResultObject = 1102;
                }
                else if (rs[0] == 0)///如果有消息就处理
                {
                    switch (rs[1])
                    {
                        case 0://"无事件"
                            result.ResultMsg = "无事件";
                            result.ResultCode = 500;
                            result.ResultObject = 0;
                            break;
                        case 2://"有新消息"
                            result.ResultCode = 200;
                            result.ResultObject = 2;
                            break;
                        case 3://"未知"
                            result.ResultCode = 200;
                            result.ResultObject = 3;
                            break;
                        case 4://"通讯录更新"
                            result.ResultCode = 200;
                            result.ResultObject = 4;
                            break;
                        case 6://"可能是红包"
                            result.ResultCode = 200;
                            result.ResultObject = 6;
                            break;
                        case 7://"在手机上操作了微信"
                            result.ResultCode = 200;
                            result.ResultObject = 7;
                            break;
                        default://其他消息
                            result.ResultCode = 200;
                            result.ResultObject = 000;
                            break;
                    }
                }
                else
                {
                    result.ResultMsg = "获得未知消息";
                    result.ResultCode = 500;
                    result.ResultObject = 0;
                }
            }
            return result;
        }
        #endregion

        #region Sync 获取服务器当前产生的消息,更新已有SyncKey(SyncCheck检测有消息产生返回状态码为200时调用)
        /// <summary>
        ///  获取服务器当前产生的消息
        /// </summary>
        /// <param name="Sid">微信认证标志(步骤3获得)</param>
        /// <param name="Skey">微信认证标志(步骤3获得)</param>
        /// <param name="Pass_ticket">微信认证标志(步骤3获得)</param>
        /// <param name="BaseRequest">所需post字段(步骤3获得)</param>
        /// <param name="_SyncKeyEntity">步骤4微信初始化获得</param>
        /// <param name="UserName">由步骤4初始化得到的当前人ID(用于获取最近联系人)</param>
        /// <param name="_CookieCollection"></param>
        /// <returns></returns>
        public ResultEntity<OutSyncEntity> Sync(string Pass_ticket, BaseRequest BaseRequest, SyncKeyEntity _SyncKeyEntity, string UserName, CookieCollection _CookieCollection)
        {
            var result = new ResultEntity<OutSyncEntity>();
            var resultmodel = new OutSyncEntity();
            var sync_url = string.Format(WXSYNCURL, BaseRequest.Sid, BaseRequest.Skey, Pass_ticket);
            Dictionary<string, object> _dic = new Dictionary<string, object>();
            _dic.Add("BaseRequest", BaseRequest);
            _dic.Add("SyncKey", _SyncKeyEntity);
            _dic.Add("rr", ~long.Parse(_CommonHelper.GetUTCMillTime(10000000)));
            string htmlStr = _CommonHelper.HttpPost(sync_url, _dic).Html;
            WebWXSyncObj wxMsgObj = Newtonsoft.Json.JsonConvert.DeserializeObject<WebWXSyncObj>(htmlStr);
            ///用当前的SyncKey替换已有的SyncKey，保证下次扫描消息获取的不是重复消息
            if (wxMsgObj != null && wxMsgObj.BaseResponse != null && wxMsgObj.BaseResponse.Ret.Equals(0))
            {
                resultmodel.SyncKey = wxMsgObj.SyncKey;
                resultmodel.AddMsgCount = wxMsgObj.AddMsgCount;
                resultmodel.AddMsgList = wxMsgObj.AddMsgList;
                if (resultmodel.AddMsgCount > 0)
                {
                    //消息初步分析处理
                    MsgHandle(resultmodel, BaseRequest, _CookieCollection,Pass_ticket,UserName);
                }
                resultmodel.ModContactCount = wxMsgObj.ModContactCount;
                resultmodel.ModContactList = wxMsgObj.ModContactList;
                result.ResultObject = resultmodel;
                result.ResultCode = 200;
            }
            return result;
        }

        #region 消息初步分析处理
        public void MsgHandle(OutSyncEntity _OutSyncEntity,BaseRequest BaseRequest, CookieCollection _CookieCollection,string Pass_ticket,string UserName)
        {
            List<AddMsgList> AddMsgList = _OutSyncEntity.AddMsgList;
            AddMsgList.ForEach(j =>
            {
                var FromUserName = j.FromUserName;
                var StatusNotifyCode = j.StatusNotifyCode.ToInt();
                var StatusNotifyUserName = j.StatusNotifyUserName.ToStr();
                var msgtype = j.MsgType.ToInt();
                var MsgId = j.MsgId;
                switch (j.MsgType)
                {
                    case (int)MsgTypeEnum.Text:
                        //群聊消息
                        if (FromUserName.IndexOf("@@") != -1)
                        {
                            var ContentLst = j.Content.Split(':');
                            j.FromGroupUser = ContentLst[0].ToStr();
                            j.Content = ContentLst[1].ToStr();
                        }
                        break;
                    case (int)MsgTypeEnum.AnimBrow:
                    case (int)MsgTypeEnum.Img:
                        j.Img = GetmsgImg(MsgId, BaseRequest.Skey, _CookieCollection);
                        break;
                    case (int)MsgTypeEnum.Voice:
                        j.File = GetVoice(MsgId, BaseRequest.Skey, _CookieCollection);
                        break;
                    case (int)MsgTypeEnum.Verify:
                        break;
                    case (int)MsgTypeEnum.PossibleFriend:
                        break;
                    case (int)MsgTypeEnum.ShareCard:
                        break;
                    case (int)MsgTypeEnum.Video:
                    case (int)MsgTypeEnum.VideoCall:
                        //获取视频封面
                        j.Img = GetmsgImg(MsgId, BaseRequest.Skey, _CookieCollection);
                        //获取视频文件流
                        j.File = GetVideo(MsgId, BaseRequest.Skey, _CookieCollection);
                        break;
                    case (int)MsgTypeEnum.Position:
                        break;
                    case (int)MsgTypeEnum.ShareUrl:
                        //接收用户文件，当为空时则
                        if (!string.IsNullOrEmpty(j.MediaId))
                        {
                            j.File = GetMedia(j.ToUserName, j.MediaId, j.FileName, j.FromUserName, Pass_ticket, _CookieCollection);
                        }
                        else 
                        {
                            //小程序不支持打开
                            if (j.AppMsgType == 33)
                            {
                                j.Content = "小程序消息,请在手机上浏览";
                            }
                            else
                            {
                                //公众号链接或其他分享
                                j.Content = j.Url;
                            }
                        }
                        break;
                    case (int)MsgTypeEnum.VOIP:
                        break;
                    case (int)MsgTypeEnum.Init:
                        //获取最近联系人信息
                        if (StatusNotifyCode == 4 && StatusNotifyUserName != "" && msgtype == 51)
                        {
                            _OutSyncEntity.LateUserList = StatusNotifyUserName.Split(',').ToList();
                        }
                        break;
                    case (int)MsgTypeEnum.VOIPNOTIFY:
                        break;
                    case (int)MsgTypeEnum.SYSNOTICE:
                        break;
                    case (int)MsgTypeEnum.System:
                        break;
                    case (int)MsgTypeEnum.Revoke:
                        j.Content = "用户撤回消息";
                        break;
                }
            });
        }
        #endregion
        #endregion

        #region SendMsg 发送文本消息
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="_BaseRequest">所需post字段(步骤3获得)</param>
        /// <param name="Pass_ticket">微信认证标志(步骤3获得)</param>
        /// <param name="fromuser">发送人ID</param>
        /// <param name="touser">接收人ID</param>
        /// <param name="Content">消息内容</param>
        /// <returns></returns>
        public ResultEntity<OutSendMsgEntity> SendMsg(BaseRequest _BaseRequest, string Pass_ticket, string fromuser, string touser, string Content)
        {
            var result = new ResultEntity<OutSendMsgEntity>();
            var resultmodel = new OutSendMsgEntity();
            var url = string.Format(WXSENDMSGURL, _BaseRequest.Sid, Pass_ticket);
            var dic = new Dictionary<string, object>();
            dic.Add("BaseRequest", _BaseRequest);
            var Msg = new { ClientMsgId = DateTime.Now.Millisecond, Content = Content, FromUserName = fromuser, LocalID = DateTime.Now.Millisecond, ToUserName = touser, Type = 1 };
            dic.Add("Msg", Msg);
            dic.Add("rr", DateTime.Now.Millisecond);
            string htmlStr = _CommonHelper.HttpPost(url, dic).Html;
            WebWXSendMsg _WebWXSendMsg = Newtonsoft.Json.JsonConvert.DeserializeObject<WebWXSendMsg>(htmlStr);
            if (_WebWXSendMsg != null)
            {
                if (_WebWXSendMsg.BaseResponse.Ret == 0)
                {
                    resultmodel.MsgID = _WebWXSendMsg.MsgID;
                    resultmodel.LocalID = _WebWXSendMsg.LocalID;
                    result.ResultObject = resultmodel;
                    result.ResultCode = 200;
                }
            }
            return result;
        }
        #endregion

        #region SendMsgImg 发送图片消息
        /// <summary>
        /// 发送图片消息
        /// </summary>
        /// <param name="_fileinfo">文件流</param>
        /// <param name="_BaseRequest">所需post字段(步骤3获得)</param>
        /// <param name="Pass_ticket">微信认证标志(步骤3获得)</param>
        /// <param name="fromuser">发送用户</param>
        /// <param name="touser">接收用户</param>
        /// <param name="_CookieCollection"></param>
        /// <returns></returns>
        public ResultEntity<OutSendMsgEntity> SendMsgImg(FileInfo _fileinfo, BaseRequest _BaseRequest, string Pass_ticket, string fromuser, string touser,CookieCollection _CookieCollection)
        {
            var result = new ResultEntity<OutSendMsgEntity>();
            var resultmodel = new OutSendMsgEntity();
            var webwx_data_ticket = "";
            foreach (Cookie cc in _CookieCollection)
            {
                if (cc.Name == "webwx_data_ticket")
                {
                    webwx_data_ticket = cc.Value.ToString();
                }
            }
            var uploadresult = UploadMedia(_fileinfo, _BaseRequest, Pass_ticket, webwx_data_ticket, fromuser, touser);
            if (uploadresult.ResultCode == 200)
            {
                var url = WXSENDMSGIMGURL;
                var dic = new Dictionary<string, object>();
                dic.Add("BaseRequest", _BaseRequest);
                var Msg = new { ClientMsgId = DateTime.Now.Millisecond, Content = "", FromUserName = fromuser, LocalID = DateTime.Now.Millisecond, MediaId = uploadresult.ResultObject.MediaId, ToUserName = touser, Type = 3 };
                dic.Add("Msg", Msg);
                dic.Add("Scene", 0);
                string htmlStr = _CommonHelper.HttpPost(url, dic, ref _CookieCollection).Html;
                WebWXSendMsg _WebWXSendMsg = Newtonsoft.Json.JsonConvert.DeserializeObject<WebWXSendMsg>(htmlStr);
                if (_WebWXSendMsg != null)
                {
                    if (_WebWXSendMsg.BaseResponse.Ret == 0)
                    {
                        resultmodel._OutUploadMediaEntity = uploadresult.ResultObject;
                        resultmodel.MsgID = _WebWXSendMsg.MsgID;
                        resultmodel.LocalID = _WebWXSendMsg.LocalID;
                        result.ResultObject = resultmodel;
                        result.ResultCode = 200;
                    }
                }
            }
            return result;
        }
        #endregion

        #region SendAppMsg 发送文件消息
        /// <summary>
        /// 发送文件消息
        /// </summary>
        /// <param name="_fileinfo">文件流</param>
        /// <param name="_BaseRequest">所需post字段(步骤3获得)</param>
        /// <param name="Pass_ticket">微信认证标志(步骤3获得)</param>
        /// <param name="fromuser">发送用户</param>
        /// <param name="touser">接收用户</param>
        /// <param name="_CookieCollection"></param>
        /// <returns></returns>
        public ResultEntity<OutSendMsgEntity> SendAppMsg(FileInfo _fileinfo, BaseRequest _BaseRequest, string Pass_ticket, string fromuser, string touser, CookieCollection _CookieCollection)
        {
            var result = new ResultEntity<OutSendMsgEntity>();
            var resultmodel = new OutSendMsgEntity();
            var webwx_data_ticket = "";
            foreach (Cookie cc in _CookieCollection)
            {
                if (cc.Name == "webwx_data_ticket")
                {
                    webwx_data_ticket = cc.Value.ToString();
                }
            }
            var uploadresult = UploadMedia(_fileinfo, _BaseRequest, Pass_ticket, webwx_data_ticket, fromuser, touser);
            if (uploadresult.ResultCode == 200)
            {
                var url = WXSENDAPPMSGURL;
                var dic = new Dictionary<string, object>();
                dic.Add("BaseRequest", _BaseRequest);
                var Content = string.Format("<appmsg appid='wxeb7ec651dd0aefa9' sdkver=''><title>{0}</title><des></des><action></action><type>6</type><content></content><url></url><lowurl></lowurl><appattach><totallen>{1}</totallen><attachid>{2}</attachid><fileext>{3}</fileext></appattach><extinfo></extinfo></appmsg>", _fileinfo.Name, _fileinfo.Length, uploadresult.ResultObject.MediaId, _fileinfo.Extension.Substring(1));
                var Msg = new { ClientMsgId = DateTime.Now.Millisecond, Content = Content, FromUserName = fromuser, LocalID = DateTime.Now.Millisecond, ToUserName = touser, Type = 6 };
                dic.Add("Msg", Msg);
                dic.Add("Scene", 0);
                string htmlStr = _CommonHelper.HttpPost(url, dic, ref _CookieCollection).Html;
                WebWXSendMsg _WebWXSendMsg = Newtonsoft.Json.JsonConvert.DeserializeObject<WebWXSendMsg>(htmlStr);
                if (_WebWXSendMsg != null)
                {
                    if (_WebWXSendMsg.BaseResponse.Ret == 0)
                    {
                        resultmodel._OutUploadMediaEntity = uploadresult.ResultObject;
                        resultmodel.MsgID = _WebWXSendMsg.MsgID;
                        resultmodel.LocalID = _WebWXSendMsg.LocalID;
                        result.ResultObject = resultmodel;
                        result.ResultCode = 200;
                    }
                }
            }
            return result;
        }
        #endregion

        #region RevokeMsg 消息撤回
        /// <summary>
        /// 消息撤回
        /// </summary>
        /// <param name="SvrMsgId">消息ID</param>
        /// <param name="ToUserName">用户ID</param>
        /// <param name="_BaseRequest">所需post字段(步骤3获得)</param>
        /// <param name="_CookieCollection"></param>
        /// <returns></returns>
        public ResultEntity<bool> RevokeMsg(string SvrMsgId, string ToUserName, BaseRequest _BaseRequest, CookieCollection _CookieCollection)
        {
            var result = new ResultEntity<bool>();
            string url = WXREVOKEMSGURL;
            Dictionary<string, object> _dic = new Dictionary<string, object>();
            _dic.Add("SvrMsgId", SvrMsgId);
            _dic.Add("ToUserName", ToUserName);
            _dic.Add("ClientMsgId", _CommonHelper.GetUTCMillTime(10000000));
            _dic.Add("BaseRequest", _BaseRequest);
            string htmlStr = _CommonHelper.HttpPost(url, _dic, ref _CookieCollection).Html;
            WXInitRoot curObj = Newtonsoft.Json.JsonConvert.DeserializeObject<WXInitRoot>(htmlStr);
            if (curObj != null && curObj.BaseResponse != null)
            {
                if (curObj.BaseResponse.Ret.Equals(0))
                {
                    result.ResultObject = true;
                    result.ResultCode = 200;
                }
                else
                {
                    result.ResultMsg = "撤回时间超时";
                    result.ResultObject = false;
                    result.ResultCode = 500;
                }
            }
            return result;
        }
        #endregion

        #region UpdateFriendsRemark 给好友添加备注
        /// <summary>
        /// 给好友添加备注
        /// </summary>
        /// <param name="UserName">好友ID</param>
        /// <param name="RemarkName">备注</param>
        /// <param name="_BaseRequest">所需post字段(步骤3获得)</param>
        /// <param name="_CookieCollection"></param>
        /// <returns></returns>
        public ResultEntity<bool> UpdateFriendsRemark(string UserName, string RemarkName, BaseRequest _BaseRequest, CookieCollection _CookieCollection)
        {
            var result = new ResultEntity<bool>();
            string url = WXOPLOGURL;
            Dictionary<string, object> _dic = new Dictionary<string, object>();
            _dic.Add("UserName", UserName);
            _dic.Add("CmdId", 2);
            _dic.Add("RemarkName", RemarkName);
            _dic.Add("BaseRequest", _BaseRequest);
            string htmlStr = _CommonHelper.HttpPost(url, _dic, ref _CookieCollection).Html;
            WXInitRoot curObj = Newtonsoft.Json.JsonConvert.DeserializeObject<WXInitRoot>(htmlStr);
            if (curObj != null && curObj.BaseResponse != null)
            {
                if (curObj.BaseResponse.Ret.Equals(0))
                {
                    result.ResultObject = true;
                    result.ResultCode = 200;
                }
            }
            return result;
        }
        #endregion

        #region UpdateChatRoom 修改群聊名称
        /// <summary>
        /// 修改群聊名称
        /// </summary>
        /// <param name="ChatRoomName">群聊ID</param>
        /// <param name="NewTopic">名称</param>
        /// <param name="_BaseRequest">所需post字段(步骤3获得)</param>
        /// <param name="_CookieCollection"></param>
        /// <returns></returns>
        public ResultEntity<bool> UpdateChatRoom(string ChatRoomName, string NewTopic, BaseRequest _BaseRequest, CookieCollection _CookieCollection)
        {
            var result = new ResultEntity<bool>();
            string url = WXUPDATECHATROOMURL;
            Dictionary<string, object> _dic = new Dictionary<string, object>();
            _dic.Add("NewTopic", NewTopic);
            _dic.Add("ChatRoomName", ChatRoomName);
            _dic.Add("BaseRequest", _BaseRequest);
            string htmlStr = _CommonHelper.HttpPost(url, _dic, ref _CookieCollection).Html;
            WXInitRoot curObj = Newtonsoft.Json.JsonConvert.DeserializeObject<WXInitRoot>(htmlStr);
            if (curObj != null && curObj.BaseResponse != null)
            {
                if (curObj.BaseResponse.Ret.Equals(0))
                {
                    result.ResultObject = true;
                    result.ResultCode = 200;
                }
            }
            return result;
        }
        #endregion

        #region Settop 置顶聊天
        /// <summary>
        /// 置顶聊天
        /// </summary>
        /// <param name="UserName">用户ID</param>
        /// <param name="_BaseRequest">所需post字段(步骤3获得)</param>
        /// <returns></returns>
        public ResultEntity<bool> Settop(string UserName, BaseRequest _BaseRequest, CookieCollection _CookieCollection)
        {
            var result = new ResultEntity<bool>();
            string url = WXOPLOGURL;
            Dictionary<string, object> _dic = new Dictionary<string, object>();
            _dic.Add("UserName", UserName);
            _dic.Add("CmdId", 3);
            _dic.Add("OP", 1);
            _dic.Add("BaseRequest", _BaseRequest);
            string htmlStr = _CommonHelper.HttpPost(url, _dic, ref _CookieCollection).Html;
            WXInitRoot curObj = Newtonsoft.Json.JsonConvert.DeserializeObject<WXInitRoot>(htmlStr);
            if (curObj != null && curObj.BaseResponse != null)
            {
                if (curObj.BaseResponse.Ret.Equals(0))
                {
                    result.ResultObject = true;
                    result.ResultCode = 200;
                }
            }
            return result;
        }
        #endregion

        #region CancelSettop 取消置顶聊天
        /// <summary>
        /// 取消置顶聊天
        /// </summary>
        /// <param name="UserName">用户ID</param>
        /// <param name="_BaseRequest">所需post字段(步骤3获得)</param>
        /// <returns></returns>
        public ResultEntity<bool> CancelSettop(string UserName, BaseRequest _BaseRequest, CookieCollection _CookieCollection)
        {
            var result = new ResultEntity<bool>();
            string url = WXOPLOGURL;
            Dictionary<string, object> _dic = new Dictionary<string, object>();
            _dic.Add("UserName", UserName);
            _dic.Add("CmdId", 3);
            _dic.Add("OP", 0);
            _dic.Add("BaseRequest", _BaseRequest);
            string htmlStr = _CommonHelper.HttpPost(url, _dic, ref _CookieCollection).Html;
            WXInitRoot curObj = Newtonsoft.Json.JsonConvert.DeserializeObject<WXInitRoot>(htmlStr);
            if (curObj != null && curObj.BaseResponse != null)
            {
                if (curObj.BaseResponse.Ret.Equals(0))
                {
                    result.ResultObject = true;
                    result.ResultCode = 200;
                }
            }
            return result;
        }
        #endregion

        #region AddUser 添加好友,微信号关注
        /// <summary>
        /// 添加好友,微信号关注
        /// </summary>
        /// <param name="_BaseRequest">所需post字段(步骤3获得)</param>
        /// <param name="_VerifyUserList">添加的用户组(VerifyUserTicket值可空)</param>
        /// <param name="VerifyContent">添加备注</param>
        /// <returns></returns>
        public ResultEntity<bool> AddUser(BaseRequest _BaseRequest, List<VerifyUserList> _VerifyUserList, string VerifyContent)
        {
            var result = new ResultEntity<bool>();
            string url = WXVERIFYUSERURL;
            Dictionary<string, object> _dic = new Dictionary<string, object>();
            _dic.Add("BaseRequest", _BaseRequest);
            _dic.Add("Opcode", 2);
            _dic.Add("VerifyUserListSize", _VerifyUserList.Count);
            _dic.Add("VerifyUserList", _VerifyUserList);
            _dic.Add("VerifyContent", VerifyContent);
            _dic.Add("SceneListCount", 1);
            var SceneList = new List<int>();
            SceneList.Add(33);
            _dic.Add("SceneList", SceneList);
            _dic.Add("skey", _BaseRequest.Skey);
            string htmlStr = _CommonHelper.HttpPost(url, _dic).Html;
            WXInitRoot curObj = Newtonsoft.Json.JsonConvert.DeserializeObject<WXInitRoot>(htmlStr);
            if (curObj != null && curObj.BaseResponse != null)
            {
                if (curObj.BaseResponse.Ret.Equals(0))
                {
                    result.ResultObject = true;
                    result.ResultCode = 200;
                }
            }
            return result;
        }
        #endregion

        #region VerifyUser 验证通过好友添加
        /// <summary>
        /// 验证通过好友添加
        /// </summary>
        /// <param name="_BaseRequest">所需post字段(步骤3获得)</param>
        /// <param name="_VerifyUserList">添加的用户组(VerifyUserTicket值为添加请求AddUser后返回值获取)</param>
        /// <returns></returns>
        public ResultEntity<bool> VerifyUser(BaseRequest _BaseRequest, List<VerifyUserList> _VerifyUserList)
        {
            var result = new ResultEntity<bool>();
            string url = WXVERIFYUSERURL;
            Dictionary<string, object> _dic = new Dictionary<string, object>();
            _dic.Add("BaseRequest", _BaseRequest);
            _dic.Add("Opcode", 3);
            _dic.Add("VerifyUserListSize", _VerifyUserList.Count);
            _dic.Add("VerifyUserList", _VerifyUserList);
            _dic.Add("VerifyContent", "");
            _dic.Add("SceneListCount", 1);
            var SceneList = new List<int>();
            SceneList.Add(33);
            _dic.Add("SceneList", SceneList);
            _dic.Add("skey", _BaseRequest.Skey);
            string htmlStr = _CommonHelper.HttpPost(url, _dic).Html;
            WXInitRoot curObj = Newtonsoft.Json.JsonConvert.DeserializeObject<WXInitRoot>(htmlStr);
            if (curObj != null && curObj.BaseResponse != null)
            {
                if (curObj.BaseResponse.Ret.Equals(0))
                {
                    result.ResultObject = true;
                    result.ResultCode = 200;
                }
            }
            return result;
        }
        #endregion

        #region LogOut 微信退出
        /// <summary>
        /// 微信退出
        /// </summary>
        /// <param name="Skey">微信认证标志(步骤3获得)</param>
        /// <param name="Sid">微信认证标志(步骤3获得)</param>
        /// <param name="Uin">微信认证标志(步骤3获得)</param>
        /// <returns></returns>
        public ResultEntity<bool> LogOut(string Skey, string Sid, string Uin)
        {
            var result = new ResultEntity<bool>();
            string url = string.Format(WXLOGINOUTURL, Skey);
            Dictionary<string, object> _dic = new Dictionary<string, object>();
            _dic.Add("sid", Sid);
            _dic.Add("uin", Uin);
            var postres = _CommonHelper.HttpPost(url, _dic);
            if (postres.StatusCode == HttpStatusCode.MovedPermanently)
            {
                result.ResultObject = true;
                result.ResultCode = 200;
            }
            return result;
        }
        #endregion


        #region UploadMedia 上传附件(除文本，表情消息以外，发送其他消息类型需先上传附件)
        public ResultEntity<OutUploadMediaEntity> UploadMedia(FileInfo fileInfo, BaseRequest _BaseRequest, string pass_ticket, string webwx_data_ticket, string fromuser, string touser)
        {
            var result = new ResultEntity<OutUploadMediaEntity>();
            var resultmodel = new OutUploadMediaEntity();
            var url = WXUPLOADMEDIAURL;
            int bufferLength = 512 * 1024;
            string datetime = DateTime.Now.ToString("ddd MMM dd yyyy HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US")) + " GMT+0800 (中国标准时间)";
            UploadMediaRequest uploadMediaRequest = new UploadMediaRequest()
            {
               UploadType = 2,
               BaseRequest = _BaseRequest,
                ClientMediaId = _CommonHelper.GetUTCMillTime(10000000).ToInt(),
                TotalLen = fileInfo.Length,
                StartPos = 0,
                DataLen = fileInfo.Length,
                MediaType = 4,
                FromUserName = fromuser,
                ToUserName = touser,
                FileMd5 = _CommonHelper.GetFileMD5Hash(fileInfo)
            };
            WXUploadMedia response = null;
            //文件大小超过512Kb，分块上传。
            if (fileInfo.Length > bufferLength)
            {
                int chunks = (int)Math.Ceiling((double)fileInfo.Length / bufferLength);
                byte[] buffer = new byte[bufferLength];
                Stream readStream = fileInfo.OpenRead();
                int chunk = 0;
                int readLength = 0;
                var fileid = _CommonHelper.GetUTCMillTime(10000000);
                while ((readLength = readStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    List<FormDataItem> dataList = new List<FormDataItem>()
                    {
                        new FormDataItem("id","WU_FILE_"+fileid),
                        new FormDataItem("name",fileInfo.Name),
                        new FormDataItem("type",MimeMapping.GetMimeMapping(fileInfo.Name)),
                        new FormDataItem("lastModifiedDate",datetime),
                        new FormDataItem("size",fileInfo.Length.ToString()),
                        new FormDataItem("chunks",chunks.ToString()),
                        new FormDataItem("chunk",chunk.ToString()),
                        new FormDataItem("mediatype",_CommonHelper.GetMediaType(fileInfo.Extension)),
                        new FormDataItem("uploadmediarequest",JsonConvert.SerializeObject(uploadMediaRequest, Newtonsoft.Json.Formatting.None)),
                        new FormDataItem("webwx_data_ticket",webwx_data_ticket),
                        new FormDataItem("pass_ticket",pass_ticket),
                        new FormDataItem("filename",fileInfo.Name,buffer,readLength)
                    };
                    string strhtml = _CommonHelper.PostMutipart(url, dataList);
                    response = JsonConvert.DeserializeObject<WXUploadMedia>(strhtml);
                    chunk++;
                }
            }
            else
            {
                byte[] buffer = new byte[fileInfo.Length];
                Stream readStream = fileInfo.OpenRead();
                int readLength = readStream.Read(buffer, 0, buffer.Length);
                List<FormDataItem> dataList = new List<FormDataItem>()
                    {
                        new FormDataItem("id","WU_FILE_"+_CommonHelper.GetUTCMillTime(10000000)),
                        new FormDataItem("name",fileInfo.Name),
                        new FormDataItem("type",MimeMapping.GetMimeMapping(fileInfo.Name)),
                        new FormDataItem("lastModifiedDate",datetime),
                        new FormDataItem("size",fileInfo.Length.ToString()),
                        new FormDataItem("mediatype",_CommonHelper.GetMediaType(fileInfo.Extension)),
                        new FormDataItem("uploadmediarequest",JsonConvert.SerializeObject(uploadMediaRequest, Newtonsoft.Json.Formatting.None)),
                        new FormDataItem("webwx_data_ticket",webwx_data_ticket),
                        new FormDataItem("pass_ticket",pass_ticket),
                        new FormDataItem("filename",fileInfo.Name,buffer,readLength)
                    };
                string strhtml = _CommonHelper.PostMutipart(url, dataList);
                response = JsonConvert.DeserializeObject<WXUploadMedia>(strhtml);
            }

            if (response != null && response.BaseResponse != null)
            {
                if (response.BaseResponse.Ret.Equals(0))
                {
                    resultmodel.MediaId = response.MediaId;
                    resultmodel.StartPos = response.StartPos;
                    resultmodel.CDNThumbImgHeight = response.CDNThumbImgHeight;
                    resultmodel.CDNThumbImgWidth = response.CDNThumbImgWidth;
                    resultmodel.EncryFileName = response.EncryFileName;
                    result.ResultObject = resultmodel;
                    result.ResultCode = 200;
                }
            }
            return result;
        }
        #endregion

        #region 图片接口
        /// <summary>
        /// 微信联系人头像图片获取
        /// </summary>
        /// <param name="UserName">用户ID</param>
        /// <param name="Skey">微信认证标志(步骤3获得)</param>
        /// <param name="_CookieCollection"></param>
        /// <returns></returns>
        public Image GeticonImg(string UserName, string Skey, CookieCollection _CookieCollection)
        {
            string url = string.Format(WXGETICONURL, UserName, Skey);
            CookieContainer _CookieContainer = new CookieContainer();
            _CookieContainer.Add(_CookieCollection);
            var img = _CommonHelper.GetImage(url, _CookieContainer);
            return img;
        }

        /// <summary>
        /// 微信群头像图片获取
        /// </summary>
        /// <param name="UserName">用户ID</param>
        /// <param name="Skey">微信认证标志(步骤3获得)</param>
        /// <param name="_CookieCollection"></param>
        /// <returns></returns>
        public Image GetheadImg(string UserName, string Skey, CookieCollection _CookieCollection)
        {
            string url = string.Format(WXGETHEADIMGURL, UserName, Skey);
            CookieContainer _CookieContainer = new CookieContainer();
            _CookieContainer.Add(_CookieCollection);
            var img = _CommonHelper.GetImage(url, _CookieContainer);
            return img;
        }

        /// <summary>
        /// 微信消息图片获取
        /// </summary>
        /// <param name="MsgID">消息ID</param>
        /// <param name="Skey">微信认证标志(步骤3获得)</param>
        /// <param name="_CookieCollection"></param>
        /// <returns></returns>
        public Image GetmsgImg(string MsgID, string Skey, CookieCollection _CookieCollection)
        {
            string url = string.Format(WXGETMSGIMGURL, MsgID, Skey);
            CookieContainer _CookieContainer = new CookieContainer();
            _CookieContainer.Add(_CookieCollection);
            var img = _CommonHelper.GetImage(url, _CookieContainer);
            return img;
        }
        #endregion

        #region 多媒体接口
        /// <summary>
        /// 语音消息
        /// </summary>
        /// <param name="MsgID">消息ID</param>
        /// <param name="Skey">微信认证标志(步骤3获得)</param>
        public byte[] GetVoice(string MsgID, string Skey, CookieCollection _CookieCollection)
        {
            string url = string.Format(WXGETVOICEURL, MsgID, Skey);
            var voice = _CommonHelper.GetFile(url, _CookieCollection).ResultByte;
            return voice;
        }

        /// <summary>
        /// 视频消息
        /// </summary>
        /// <param name="MsgID">消息ID</param>
        /// <param name="Skey">微信认证标志(步骤3获得)</param>
        public byte[] GetVideo(string MsgID, string Skey, CookieCollection _CookieCollection)
        {
            string url = string.Format(WXGETVIDEOURL, MsgID, Skey);
            var video = _CommonHelper.GetFile(url, _CookieCollection).ResultByte;
            return video;
        }

        /// <summary>
        /// 其他文件
        /// </summary>
        /// <param name="sender">用户ID</param>
        /// <param name="mediaid">文件ID</param>
        /// <param name="filename">文件名</param>
        /// <param name="fromuser">来自用户</param>
        /// <param name="pass_ticket">微信认证标志(步骤3获得)</param>
        /// <param name="webwx_data_ticket">cookie中信息</param>
        public byte[] GetMedia(string sender, string mediaid, string filename, string fromuser, string pass_ticket, CookieCollection _CookieCollection)
        {
            var webwx_data_ticket = "";
            foreach (Cookie cc in _CookieCollection)
            {
                if (cc.Name == "webwx_data_ticket")
                {
                    webwx_data_ticket = cc.Value.ToString();
                }
            }
            string url = string.Format(WXGETMEDIAURL, sender, mediaid, filename, fromuser, pass_ticket, webwx_data_ticket);
            var media = _CommonHelper.GetFile(url, _CookieCollection).ResultByte;
            return media;
        }
        #endregion
    }
}
