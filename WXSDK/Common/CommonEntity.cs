using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using WXSDK.Models;

namespace WXSDK.Common
{
    public class BaseResponse
    {
        /// <summary>
        /// Ret
        /// </summary>
        public long Ret { get; set; }
        /// <summary>
        /// ErrMsg
        /// </summary>
        public string ErrMsg { get; set; }
        /// <summary>
        /// 全部信息字典
        /// </summary>
        public static Dictionary<string, MemberObjEntity> MemberObjList = new Dictionary<string, MemberObjEntity>();
    }

    /// <summary>
    /// 获取群成员和联系人信息返回的对象
    /// </summary>
    public class WXGroupuserList
    {
        public BaseResponse BaseResponse { get; set; }

        public long Count { get; set; }

        public List<MemberObjEntity> ContactList { get; set; }
    }

   
    public class MemberBaseuser
    {
        public string GetDisName()
        {
            if (string.IsNullOrEmpty(DisplayName) == false)
            {
                return DisplayName;
            }
            if (string.IsNullOrEmpty(NickName) == false)
            {
                return NickName;
            }
            return UserName;
        }
        /// <summary>
        /// Uin
        /// </summary>
        public long Uin { get; set; }
        /// <summary>
        /// UserName @e2fd3cab73debd0364179e86fa2ff114
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// NickName
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// AttrStatus
        /// </summary>
        public long AttrStatus { get; set; }
        /// <summary>
        /// PYInitial
        /// </summary>
        public string PYInitial { get; set; }
        /// <summary>
        /// PYQuanPin
        /// </summary>
        public string PYQuanPin { get; set; }
        /// <summary>
        /// RemarkPYInitial
        /// </summary>
        public string RemarkPYInitial { get; set; }
        /// <summary>
        /// RemarkPYQuanPin
        /// </summary>
        public string RemarkPYQuanPin { get; set; }
        /// <summary>
        /// MemberStatus
        /// </summary>
        public long MemberStatus { get; set; }
        /// <summary>
        /// DisplayName
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// KeyWord
        /// </summary>
        public string KeyWord { get; set; }
    }
    /// <summary>
    /// 个人信息
    /// </summary>
    public class UserInfo
    {
        public string GetDisName()
        {
            if (string.IsNullOrEmpty(RemarkName) == false)
            {
                return RemarkName;
            }
            if (string.IsNullOrEmpty(NickName) == false)
            {
                return NickName;
            }
            return UserName;
        }
        /// <summary>
        /// Uin
        /// </summary>
        public long Uin { get; set; }
        /// <summary>
        /// UserName
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// NickName
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// /cgi-bin/mmwebwx-bin/webwxgeticon?seq=0&username=filehelper&skey=@crypt_fe6bb81e_02604a7caf94b8167a58dcbf17d32356
        /// </summary>
        public string HeadImgUrl { get; set; }
        /// <summary>
        /// ContactFlag
        /// </summary>
        public long ContactFlag { get; set; }
        /// <summary>
        /// MemberCount
        /// </summary>
        public long MemberCount { get; set; }
        /// <summary>
        /// MemberList
        /// </summary>
        public List<MemberBaseEntity> MemberList { get; set; }
        /// <summary>
        /// RemarkName
        /// </summary>
        public string RemarkName { get; set; }
        /// <summary>
        /// HideInputBarFlag
        /// </summary>
        public long HideInputBarFlag { get; set; }
        /// <summary>
        /// Sex
        /// </summary>
        public long Sex { get; set; }
        /// <summary>
        /// Signature
        /// </summary>
        public string Signature { get; set; }
        /// <summary>
        /// VerifyFlag
        /// </summary>
        public long VerifyFlag { get; set; }
        /// <summary>
        /// OwnerUin
        /// </summary>
        public long OwnerUin { get; set; }
        /// <summary>
        /// PYInitial
        /// </summary>
        public string PYInitial { get; set; }
        /// <summary>
        /// PYQuanPin
        /// </summary>
        public string PYQuanPin { get; set; }
        /// <summary>
        /// RemarkPYInitial
        /// </summary>
        public string RemarkPYInitial { get; set; }
        /// <summary>
        /// RemarkPYQuanPin
        /// </summary>
        public string RemarkPYQuanPin { get; set; }
        /// <summary>
        /// StarFriend
        /// </summary>
        public long StarFriend { get; set; }
        /// <summary>
        /// AppAccountFlag
        /// </summary>
        public long AppAccountFlag { get; set; }
        /// <summary>
        /// Statues
        /// </summary>
        public long Statues { get; set; }
        /// <summary>
        /// AttrStatus
        /// </summary>
        public long AttrStatus { get; set; }
        /// <summary>
        /// Province
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// City
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Alias
        /// </summary>
        public string Alias { get; set; }
        /// <summary>
        /// SnsFlag
        /// </summary>
        public long SnsFlag { get; set; }
        /// <summary>
        /// UniFriend
        /// </summary>
        public long UniFriend { get; set; }
        /// <summary>
        /// DisplayName
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// ChatRoomId
        /// </summary>
        public long ChatRoomId { get; set; }
        /// <summary>
        /// fil
        /// </summary>
        public string KeyWord { get; set; }
        /// <summary>
        /// EncryChatRoomId
        /// </summary>
        public string EncryChatRoomId { get; set; }
        /// <summary>
        /// IsOwner
        /// </summary>
        public long IsOwner { get; set; }
    }
    /// <summary>
    /// 群员信息
    /// </summary>
    public class Groupuser
    {
        public string GetDisName()
        {
            if (string.IsNullOrEmpty(RemarkName) == false)
            {
                return RemarkName;
            }
            if (string.IsNullOrEmpty(NickName) == false)
            {
                return NickName;
            }
            return UserName;
        }
        /// <summary>
        /// Uin
        /// </summary>
        public long Uin { get; set; }
        /// <summary>
        /// filehelper
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// NickName
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// /cgi-bin/mmwebwx-bin/webwxgeticon?seq=0&username=filehelper&skey=@crypt_fe6bb81e_02604a7caf94b8167a58dcbf17d32356
        /// </summary>
        public string HeadImgUrl { get; set; }
        /// <summary>
        /// ContactFlag
        /// </summary>
        public long ContactFlag { get; set; }
        /// <summary>
        /// MemberCount
        /// </summary>
        public long MemberCount { get; set; }
        /// <summary>
        /// MemberList
        /// </summary>
        public List<MemberBaseEntity> MemberList { get; set; }
        /// <summary>
        /// RemarkName
        /// </summary>
        public string RemarkName { get; set; }
        /// <summary>
        /// HideInputBarFlag
        /// </summary>
        public long HideInputBarFlag { get; set; }
        /// <summary>
        /// Sex
        /// </summary>
        public long Sex { get; set; }
        /// <summary>
        /// Signature
        /// </summary>
        public string Signature { get; set; }
        /// <summary>
        /// VerifyFlag
        /// </summary>
        public long VerifyFlag { get; set; }
        /// <summary>
        /// OwnerUin
        /// </summary>
        public long OwnerUin { get; set; }
        /// <summary>
        /// PYInitial
        /// </summary>
        public string PYInitial { get; set; }
        /// <summary>
        /// PYQuanPin
        /// </summary>
        public string PYQuanPin { get; set; }
        /// <summary>
        /// RemarkPYInitial
        /// </summary>
        public string RemarkPYInitial { get; set; }
        /// <summary>
        /// RemarkPYQuanPin
        /// </summary>
        public string RemarkPYQuanPin { get; set; }
        /// <summary>
        /// StarFriend
        /// </summary>
        public long StarFriend { get; set; }
        /// <summary>
        /// AppAccountFlag
        /// </summary>
        public long AppAccountFlag { get; set; }
        /// <summary>
        /// Statues
        /// </summary>
        public long Statues { get; set; }
        /// <summary>
        /// AttrStatus
        /// </summary>
        public long AttrStatus { get; set; }
        /// <summary>
        /// Province
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// City
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Alias
        /// </summary>
        public string Alias { get; set; }
        /// <summary>
        /// SnsFlag
        /// </summary>
        public long SnsFlag { get; set; }
        /// <summary>
        /// UniFriend
        /// </summary>
        public long UniFriend { get; set; }
        /// <summary>
        /// DisplayName
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// ChatRoomId
        /// </summary>
        public long ChatRoomId { get; set; }
        /// <summary>
        /// KeyWord
        /// </summary>
        public string KeyWord { get; set; }
        /// <summary>
        /// EncryChatRoomId
        /// </summary>
        public string EncryChatRoomId { get; set; }
        /// <summary>
        /// IsOwner
        /// </summary>
        public long IsOwner { get; set; }
    }
    /// <summary>
    /// 全部信息
    /// </summary>
    public class GroupChatUser
    {
        public string GetDisName()
        {
            if (string.IsNullOrEmpty(RemarkName) == false)
            {
                return RemarkName;
            }
            if (string.IsNullOrEmpty(NickName) == false)
            {
                return NickName;
            }
            return UserName;
        }
        /// <summary>
        /// Uin 0
        /// </summary>
        public long Uin { get; set; }
        /// <summary>
        /// UserName@5a0bf58bcf2f7ceb6386ec84f8f81979afabe48779a599ce06b040849033085
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// NickName
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// /cgi-bin/mmwebwx-bin/webwxgeticon?seq=0&username=filehelper&skey=@crypt_fe6bb81e_02604a7caf94b8167a58dcbf17d32356
        /// </summary>
        public string HeadImgUrl { get; set; }
        /// <summary>
        /// ContactFlag 1
        /// </summary>
        public long ContactFlag { get; set; }
        /// <summary>
        /// MemberCount 0
        /// </summary>
        public long MemberCount { get; set; }
        /// <summary>
        /// MemberList 【】
        /// </summary>
        public List<MemberBaseuser> MemberList { get; set; }
        /// <summary>
        /// RemarkName
        /// </summary>
        public string RemarkName { get; set; }
        /// <summary>
        /// HideInputBarFlag 0
        /// </summary>
        public long HideInputBarFlag { get; set; }
        /// <summary>
        /// Sex 0,1
        /// </summary>
        public long Sex { get; set; }
        /// <summary>
        /// Signature 签名
        /// </summary>
        public string Signature { get; set; }
        /// <summary>
        /// VerifyFlag
        /// </summary>
        public long VerifyFlag { get; set; }
        /// <summary>
        /// OwnerUin
        /// </summary>
        public long OwnerUin { get; set; }
        /// <summary>
        /// PYInitial
        /// </summary>
        public string PYInitial { get; set; }
        /// <summary>
        /// PYQuanPin
        /// </summary>
        public string PYQuanPin { get; set; }
        /// <summary>
        /// RemarkPYInitial
        /// </summary>
        public string RemarkPYInitial { get; set; }
        /// <summary>
        /// RemarkPYQuanPin
        /// </summary>
        public string RemarkPYQuanPin { get; set; }
        /// <summary>
        /// StarFriend
        /// </summary>
        public long StarFriend { get; set; }
        /// <summary>
        /// AppAccountFlag
        /// </summary>
        public long AppAccountFlag { get; set; }
        /// <summary>
        /// Statues
        /// </summary>
        public long Statues { get; set; }
        /// <summary>
        /// AttrStatus
        /// </summary>
        public long AttrStatus { get; set; }
        /// <summary>
        /// Province
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// City
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Alias
        /// </summary>
        public string Alias { get; set; }
        /// <summary>
        /// SnsFlag
        /// </summary>
        public long SnsFlag { get; set; }
        /// <summary>
        /// UniFriend
        /// </summary>
        public long UniFriend { get; set; }
        /// <summary>
        /// DisplayName
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// ChatRoomId
        /// </summary>
        public long ChatRoomId { get; set; }
        /// <summary>
        /// KeyWord
        /// </summary>
        public string KeyWord { get; set; }
        /// <summary>
        /// EncryChatRoomId
        /// </summary>
        public string EncryChatRoomId { get; set; }
        /// <summary>
        /// IsOwner
        /// </summary>
        public long IsOwner { get; set; }
    }

    /// <summary>
    /// 网页登录微信成功后返回的对象
    /// </summary>
    public class WXInitRoot
    {
        /// <summary>
        /// BaseResponse
        /// </summary>
        public BaseResponse BaseResponse { get; set; }
        /// <summary>
        /// Count
        /// </summary>
        public long Count { get; set; }
        /// <summary>
        /// ContactList
        /// </summary>
        public List<MemberObjEntity> ContactList { get; set; }
        /// <summary>
        /// SyncKey
        /// </summary>
        public SyncKeyEntity SyncKey { get; set; }
        /// <summary>
        /// User
        /// </summary>
        public UserEntity User { get; set; }
        /// <summary>
        /// filehelper,weixin,
        /// </summary>
        public string ChatSet { get; set; }
        /// <summary>
        /// @crypt_fe6bb81e_02604a7caf94b8167a58dcbf17d32356
        /// </summary>
        public string SKey { get; set; }
        /// <summary>
        /// ClientVersion
        /// </summary>
        public long ClientVersion { get; set; }
        /// <summary>
        /// SystemTime
        /// </summary>
        public long SystemTime { get; set; }
        /// <summary>
        /// GrayScale
        /// </summary>
        public long GrayScale { get; set; }
        /// <summary>
        /// InviteStartCount
        /// </summary>
        public long InviteStartCount { get; set; }
        /// <summary>
        /// MPSubscribeMsgCount
        /// </summary>
        public long MPSubscribeMsgCount { get; set; }
        /// <summary>
        /// MPSubscribeMsgList
        /// </summary>
        public List<Object> MPSubscribeMsgList { get; set; }
        /// <summary>
        /// ClickReportInterval
        /// </summary>
        public long ClickReportInterval { get; set; }
    }

    /// <summary>
    /// 获取所有用户返回的对象
    /// </summary>
    public class WebWXMemberObject
    {
        public BaseResponse BaseResponse { get; set; }
        /// <summary>
        /// MemberCount
        /// </summary>
        public int MemberCount { get; set; }
        /// <summary>
        /// MemberList
        /// </summary>
        public List<MemberObjEntity> MemberList { get; set; }
        /// <summary>
        /// 普通用户列表
        /// </summary>
        public List<MemberObjEntity> MemberListUser { get; set; }
        /// <summary>
        /// 公众号列表
        /// </summary>
        public List<MemberObjEntity> MemberListGroup { get; set; }
        /// <summary>
        /// 群聊列表
        /// </summary>
        public List<MemberObjEntity> GroupChatList { get; set; }
        /// <summary>
        /// 个人列表
        /// </summary>
        public List<UserEntity> UsererList { get; set; }
        /// <summary>
        /// Seq
        /// </summary>
        public int Seq { get; set; }
        /// <summary>
        /// 普通成员
        /// </summary>
        public List<MemberObjEntity> NormalMember = new List<MemberObjEntity>();

        /// <summary>
        /// 公众号
        /// </summary>
        public List<MemberObjEntity> PublicMember = new List<MemberObjEntity>();

        /// <summary>
        /// 群聊
        /// </summary>
        public List<MemberObjEntity> GroupMember = new List<MemberObjEntity>();
        /// <summary>
        /// 其他用户
        /// </summary>
        public List<MemberObjEntity> OtherMember = new List<MemberObjEntity>();

        /// <summary>
        /// 对用户进行分类
        /// </summary>
        public void SortMembers()
        {
            string[] special_users = new string[]{"newsapp", "fmessage", "filehelper", "weibo", "qqmail",
                         "fmessage", "tmessage", "qmessage", "qqsync", "floatbottle",
                         "lbsapp", "shakeapp", "medianote", "qqfriend", "readerapp",
                         "blogapp", "facebookapp", "masssendapp", "meishiapp",
                         "feedsapp", "voip", "blogappweixin", "weixin", "brandsessionholder",
                         "weixinreminder", "wxid_novlwrv3lqwv11", "gh_22b87fa7cb3c",
                         "officialaccounts", "notification_messages", "wxid_novlwrv3lqwv11",
                         "gh_22b87fa7cb3c", "wxitil", "userexperience_alarm", "notification_messages"};
            if (MemberList != null)
            {
                foreach (MemberObjEntity oneMem in MemberList)
                {
                    if (((int)oneMem.VerifyFlag & 8) != 0)
                    {

                        PublicMember.Add(oneMem);
                    }
                    else if (special_users.Contains(oneMem.UserName))
                    {

                        OtherMember.Add(oneMem);


                    }
                    else if (oneMem.UserName.IndexOf("@@") != -1)
                    {

                        GroupMember.Add(oneMem);

                    }
                    else
                    {

                        NormalMember.Add(oneMem);

                    }
                }
            }
        }
    }

    public class WebWXSyncObj
    {
        /// <summary>
        /// BaseResponse
        /// </summary>
        public BaseResponse BaseResponse { get; set; }
        /// <summary>
        /// AddMsgCount
        /// </summary>
        public long AddMsgCount { get; set; }
        /// <summary>
        /// AddMsgList
        /// </summary>
        public List<AddMsgList> AddMsgList { get; set; }
        /// <summary>
        /// ModContactCount
        /// </summary>
        public long ModContactCount { get; set; }
        /// <summary>
        /// ModContactList
        /// </summary>
        public List<ModContactList> ModContactList { get; set; }
        /// <summary>
        /// DelContactCount
        /// </summary>
        public long DelContactCount { get; set; }
        /// <summary>
        /// DelContactList
        /// </summary>
        public List<ModContactList> DelContactList { get; set; }
        /// <summary>
        /// ModChatRoomMemberCount
        /// </summary>
        public long ModChatRoomMemberCount { get; set; }
        /// <summary>
        /// ModChatRoomMemberList
        /// </summary>
        public List<ModContactList> ModChatRoomMemberList { get; set; }
        /// <summary>
        /// Profile
        /// </summary>
        public Profile Profile { get; set; }
        /// <summary>
        /// ContinueFlag
        /// </summary>
        public long ContinueFlag { get; set; }
        /// <summary>
        /// SyncKey
        /// </summary>
        public SyncKeyEntity SyncKey { get; set; }
        /// <summary>
        /// SKey
        /// </summary>
        public string SKey { get; set; }
        /// <summary>
        /// SyncCheckKey
        /// </summary>
        public SyncKeyEntity SyncCheckKey { get; set; }
    }

    public class Profile
    {
        /// <summary>
        /// BitFlag
        /// </summary>
        public long BitFlag { get; set; }
        /// <summary>
        /// UserName
        /// </summary>
        public UserName UserName { get; set; }
        /// <summary>
        /// NickName
        /// </summary>
        public NickName NickName { get; set; }
        /// <summary>
        /// BindUin
        /// </summary>
        public long BindUin { get; set; }
        /// <summary>
        /// BindEmail
        /// </summary>
        public BindEmail BindEmail { get; set; }
        /// <summary>
        /// BindMobile
        /// </summary>
        public BindMobile BindMobile { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        public long Status { get; set; }
        /// <summary>
        /// Sex
        /// </summary>
        public long Sex { get; set; }
        /// <summary>
        /// PersonalCard
        /// </summary>
        public long PersonalCard { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Alias { get; set; }
        /// <summary>
        /// HeadImgUpdateFlag
        /// </summary>
        public long HeadImgUpdateFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string HeadImgUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Signature { get; set; }
    }

    public class UserName
    {
        /// <summary>
        /// 
        /// </summary>
        public string Buff { get; set; }
    }

    public class NickName
    {
        /// <summary>
        /// 
        /// </summary>
        public string Buff { get; set; }
    }

    public class BindEmail
    {
        /// <summary>
        /// 
        /// </summary>
        public string Buff { get; set; }
    }

    public class BindMobile
    {
        /// <summary>
        /// 
        /// </summary>
        public string Buff { get; set; }
    }

    public class WebWXSendMsg
    {
        public BaseResponse BaseResponse { get; set; }

        public string MsgID { get; set; }

        public string LocalID { get; set; }
    }

    public class WXUploadMedia
    {
        public BaseResponse BaseResponse { get; set; }
        public string MediaId { get; set; }
        public int StartPos { get; set; }
        public int CDNThumbImgHeight { get; set; }
        public int CDNThumbImgWidth { get; set; }
        public string EncryFileName { get; set; }
    }

    public class FormDataItem
    {
        public string Name;
        public string Value;
        public bool isFile;
        public string FileName;
        public byte[] Content;
        public int ContentLength;

        public FormDataItem(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public FormDataItem(string name, string filename, byte[] content, int length)
        {
            isFile = true;
            Name = name;
            FileName = filename;
            Content = content;
            ContentLength = length;
        }
    }

    public static class MimeMapping
    {
        private static Dictionary<string, string> _mimeMappingTable;

        private static void AddMimeMapping(string extension, string MimeType)
        {
            MimeMapping._mimeMappingTable.Add(extension, MimeType);
        }

        public static string GetMimeMapping(string FileName)
        {
            string text = null;
            int num = FileName.LastIndexOf('.');
            if (0 < num && num > FileName.LastIndexOf('\\'))
            {
                text = (string)MimeMapping._mimeMappingTable[FileName.Substring(num)];
            }
            if (text == null)
            {
                text = (string)MimeMapping._mimeMappingTable[".*"];
            }
            return text;
        }

        static MimeMapping()
        {
            MimeMapping._mimeMappingTable = new Dictionary<string, string>(190, StringComparer.CurrentCultureIgnoreCase);
            MimeMapping.AddMimeMapping(".323", "text/h323");
            MimeMapping.AddMimeMapping(".asx", "video/x-ms-asf");
            MimeMapping.AddMimeMapping(".acx", "application/internet-property-stream");
            MimeMapping.AddMimeMapping(".ai", "application/postscript");
            MimeMapping.AddMimeMapping(".aif", "audio/x-aiff");
            MimeMapping.AddMimeMapping(".aiff", "audio/aiff");
            MimeMapping.AddMimeMapping(".axs", "application/olescript");
            MimeMapping.AddMimeMapping(".aifc", "audio/aiff");
            MimeMapping.AddMimeMapping(".asr", "video/x-ms-asf");
            MimeMapping.AddMimeMapping(".avi", "video/x-msvideo");
            MimeMapping.AddMimeMapping(".asf", "video/x-ms-asf");
            MimeMapping.AddMimeMapping(".au", "audio/basic");
            MimeMapping.AddMimeMapping(".application", "application/x-ms-application");
            MimeMapping.AddMimeMapping(".bin", "application/octet-stream");
            MimeMapping.AddMimeMapping(".bas", "text/plain");
            MimeMapping.AddMimeMapping(".bcpio", "application/x-bcpio");
            MimeMapping.AddMimeMapping(".bmp", "image/bmp");
            MimeMapping.AddMimeMapping(".cdf", "application/x-cdf");
            MimeMapping.AddMimeMapping(".cat", "application/vndms-pkiseccat");
            MimeMapping.AddMimeMapping(".crt", "application/x-x509-ca-cert");
            MimeMapping.AddMimeMapping(".c", "text/plain");
            MimeMapping.AddMimeMapping(".css", "text/css");
            MimeMapping.AddMimeMapping(".cer", "application/x-x509-ca-cert");
            MimeMapping.AddMimeMapping(".crl", "application/pkix-crl");
            MimeMapping.AddMimeMapping(".cmx", "image/x-cmx");
            MimeMapping.AddMimeMapping(".csh", "application/x-csh");
            MimeMapping.AddMimeMapping(".cod", "image/cis-cod");
            MimeMapping.AddMimeMapping(".cpio", "application/x-cpio");
            MimeMapping.AddMimeMapping(".clp", "application/x-msclip");
            MimeMapping.AddMimeMapping(".crd", "application/x-mscardfile");
            MimeMapping.AddMimeMapping(".deploy", "application/octet-stream");
            MimeMapping.AddMimeMapping(".dll", "application/x-msdownload");
            MimeMapping.AddMimeMapping(".dot", "application/msword");
            MimeMapping.AddMimeMapping(".doc", "application/msword");
            MimeMapping.AddMimeMapping(".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
            MimeMapping.AddMimeMapping(".dvi", "application/x-dvi");
            MimeMapping.AddMimeMapping(".dir", "application/x-director");
            MimeMapping.AddMimeMapping(".dxr", "application/x-director");
            MimeMapping.AddMimeMapping(".der", "application/x-x509-ca-cert");
            MimeMapping.AddMimeMapping(".dib", "image/bmp");
            MimeMapping.AddMimeMapping(".dcr", "application/x-director");
            MimeMapping.AddMimeMapping(".disco", "text/xml");
            MimeMapping.AddMimeMapping(".exe", "application/octet-stream");
            MimeMapping.AddMimeMapping(".etx", "text/x-setext");
            MimeMapping.AddMimeMapping(".evy", "application/envoy");
            MimeMapping.AddMimeMapping(".eml", "message/rfc822");
            MimeMapping.AddMimeMapping(".eps", "application/postscript");
            MimeMapping.AddMimeMapping(".flr", "x-world/x-vrml");
            MimeMapping.AddMimeMapping(".fif", "application/fractals");
            MimeMapping.AddMimeMapping(".gtar", "application/x-gtar");
            MimeMapping.AddMimeMapping(".gif", "image/gif");
            MimeMapping.AddMimeMapping(".gz", "application/x-gzip");
            MimeMapping.AddMimeMapping(".hta", "application/hta");
            MimeMapping.AddMimeMapping(".htc", "text/x-component");
            MimeMapping.AddMimeMapping(".htt", "text/webviewhtml");
            MimeMapping.AddMimeMapping(".h", "text/plain");
            MimeMapping.AddMimeMapping(".hdf", "application/x-hdf");
            MimeMapping.AddMimeMapping(".hlp", "application/winhlp");
            MimeMapping.AddMimeMapping(".html", "text/html");
            MimeMapping.AddMimeMapping(".htm", "text/html");
            MimeMapping.AddMimeMapping(".hqx", "application/mac-binhex40");
            MimeMapping.AddMimeMapping(".isp", "application/x-internet-signup");
            MimeMapping.AddMimeMapping(".iii", "application/x-iphone");
            MimeMapping.AddMimeMapping(".ief", "image/ief");
            MimeMapping.AddMimeMapping(".ivf", "video/x-ivf");
            MimeMapping.AddMimeMapping(".ins", "application/x-internet-signup");
            MimeMapping.AddMimeMapping(".ico", "image/x-icon");
            MimeMapping.AddMimeMapping(".jpg", "image/jpeg");
            MimeMapping.AddMimeMapping(".jfif", "image/pjpeg");
            MimeMapping.AddMimeMapping(".jpe", "image/jpeg");
            MimeMapping.AddMimeMapping(".jpeg", "image/jpeg");
            MimeMapping.AddMimeMapping(".js", "application/x-javascript");
            MimeMapping.AddMimeMapping(".lsx", "video/x-la-asf");
            MimeMapping.AddMimeMapping(".latex", "application/x-latex");
            MimeMapping.AddMimeMapping(".lsf", "video/x-la-asf");
            MimeMapping.AddMimeMapping(".manifest", "application/x-ms-manifest");
            MimeMapping.AddMimeMapping(".mhtml", "message/rfc822");
            MimeMapping.AddMimeMapping(".mny", "application/x-msmoney");
            MimeMapping.AddMimeMapping(".mht", "message/rfc822");
            MimeMapping.AddMimeMapping(".mid", "audio/mid");
            MimeMapping.AddMimeMapping(".mpv2", "video/mpeg");
            MimeMapping.AddMimeMapping(".man", "application/x-troff-man");
            MimeMapping.AddMimeMapping(".mvb", "application/x-msmediaview");
            MimeMapping.AddMimeMapping(".mpeg", "video/mpeg");
            MimeMapping.AddMimeMapping(".m3u", "audio/x-mpegurl");
            MimeMapping.AddMimeMapping(".mdb", "application/x-msaccess");
            MimeMapping.AddMimeMapping(".mpp", "application/vnd.ms-project");
            MimeMapping.AddMimeMapping(".m1v", "video/mpeg");
            MimeMapping.AddMimeMapping(".mpa", "video/mpeg");
            MimeMapping.AddMimeMapping(".me", "application/x-troff-me");
            MimeMapping.AddMimeMapping(".m13", "application/x-msmediaview");
            MimeMapping.AddMimeMapping(".movie", "video/x-sgi-movie");
            MimeMapping.AddMimeMapping(".m14", "application/x-msmediaview");
            MimeMapping.AddMimeMapping(".mpe", "video/mpeg");
            MimeMapping.AddMimeMapping(".mp2", "video/mpeg");
            MimeMapping.AddMimeMapping(".mov", "video/quicktime");
            MimeMapping.AddMimeMapping(".mp3", "audio/mpeg");
            MimeMapping.AddMimeMapping(".mpg", "video/mpeg");
            MimeMapping.AddMimeMapping(".ms", "application/x-troff-ms");
            MimeMapping.AddMimeMapping(".nc", "application/x-netcdf");
            MimeMapping.AddMimeMapping(".nws", "message/rfc822");
            MimeMapping.AddMimeMapping(".oda", "application/oda");
            MimeMapping.AddMimeMapping(".ods", "application/oleobject");
            MimeMapping.AddMimeMapping(".pmc", "application/x-perfmon");
            MimeMapping.AddMimeMapping(".p7r", "application/x-pkcs7-certreqresp");
            MimeMapping.AddMimeMapping(".p7b", "application/x-pkcs7-certificates");
            MimeMapping.AddMimeMapping(".p7s", "application/pkcs7-signature");
            MimeMapping.AddMimeMapping(".pmw", "application/x-perfmon");
            MimeMapping.AddMimeMapping(".ps", "application/postscript");
            MimeMapping.AddMimeMapping(".p7c", "application/pkcs7-mime");
            MimeMapping.AddMimeMapping(".pbm", "image/x-portable-bitmap");
            MimeMapping.AddMimeMapping(".ppm", "image/x-portable-pixmap");
            MimeMapping.AddMimeMapping(".pub", "application/x-mspublisher");
            MimeMapping.AddMimeMapping(".pnm", "image/x-portable-anymap");
            MimeMapping.AddMimeMapping(".png", "image/png");
            MimeMapping.AddMimeMapping(".pml", "application/x-perfmon");
            MimeMapping.AddMimeMapping(".p10", "application/pkcs10");
            MimeMapping.AddMimeMapping(".pfx", "application/x-pkcs12");
            MimeMapping.AddMimeMapping(".p12", "application/x-pkcs12");
            MimeMapping.AddMimeMapping(".pdf", "application/pdf");
            MimeMapping.AddMimeMapping(".pps", "application/vnd.ms-powerpoint");
            MimeMapping.AddMimeMapping(".p7m", "application/pkcs7-mime");
            MimeMapping.AddMimeMapping(".pko", "application/vndms-pkipko");
            MimeMapping.AddMimeMapping(".ppt", "application/vnd.ms-powerpoint");
            MimeMapping.AddMimeMapping(".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation");
            MimeMapping.AddMimeMapping(".pmr", "application/x-perfmon");
            MimeMapping.AddMimeMapping(".pma", "application/x-perfmon");
            MimeMapping.AddMimeMapping(".pot", "application/vnd.ms-powerpoint");
            MimeMapping.AddMimeMapping(".prf", "application/pics-rules");
            MimeMapping.AddMimeMapping(".pgm", "image/x-portable-graymap");
            MimeMapping.AddMimeMapping(".qt", "video/quicktime");
            MimeMapping.AddMimeMapping(".ra", "audio/x-pn-realaudio");
            MimeMapping.AddMimeMapping(".rgb", "image/x-rgb");
            MimeMapping.AddMimeMapping(".ram", "audio/x-pn-realaudio");
            MimeMapping.AddMimeMapping(".rmi", "audio/mid");
            MimeMapping.AddMimeMapping(".ras", "image/x-cmu-raster");
            MimeMapping.AddMimeMapping(".roff", "application/x-troff");
            MimeMapping.AddMimeMapping(".rtf", "application/rtf");
            MimeMapping.AddMimeMapping(".rtx", "text/richtext");
            MimeMapping.AddMimeMapping(".sv4crc", "application/x-sv4crc");
            MimeMapping.AddMimeMapping(".spc", "application/x-pkcs7-certificates");
            MimeMapping.AddMimeMapping(".setreg", "application/set-registration-initiation");
            MimeMapping.AddMimeMapping(".snd", "audio/basic");
            MimeMapping.AddMimeMapping(".stl", "application/vndms-pkistl");
            MimeMapping.AddMimeMapping(".setpay", "application/set-payment-initiation");
            MimeMapping.AddMimeMapping(".stm", "text/html");
            MimeMapping.AddMimeMapping(".shar", "application/x-shar");
            MimeMapping.AddMimeMapping(".sh", "application/x-sh");
            MimeMapping.AddMimeMapping(".sit", "application/x-stuffit");
            MimeMapping.AddMimeMapping(".spl", "application/futuresplash");
            MimeMapping.AddMimeMapping(".sct", "text/scriptlet");
            MimeMapping.AddMimeMapping(".scd", "application/x-msschedule");
            MimeMapping.AddMimeMapping(".sst", "application/vndms-pkicertstore");
            MimeMapping.AddMimeMapping(".src", "application/x-wais-source");
            MimeMapping.AddMimeMapping(".sv4cpio", "application/x-sv4cpio");
            MimeMapping.AddMimeMapping(".tex", "application/x-tex");
            MimeMapping.AddMimeMapping(".tgz", "application/x-compressed");
            MimeMapping.AddMimeMapping(".t", "application/x-troff");
            MimeMapping.AddMimeMapping(".tar", "application/x-tar");
            MimeMapping.AddMimeMapping(".tr", "application/x-troff");
            MimeMapping.AddMimeMapping(".tif", "image/tiff");
            MimeMapping.AddMimeMapping(".txt", "text/plain");
            MimeMapping.AddMimeMapping(".texinfo", "application/x-texinfo");
            MimeMapping.AddMimeMapping(".trm", "application/x-msterminal");
            MimeMapping.AddMimeMapping(".tiff", "image/tiff");
            MimeMapping.AddMimeMapping(".tcl", "application/x-tcl");
            MimeMapping.AddMimeMapping(".texi", "application/x-texinfo");
            MimeMapping.AddMimeMapping(".tsv", "text/tab-separated-values");
            MimeMapping.AddMimeMapping(".ustar", "application/x-ustar");
            MimeMapping.AddMimeMapping(".uls", "text/iuls");
            MimeMapping.AddMimeMapping(".vcf", "text/x-vcard");
            MimeMapping.AddMimeMapping(".wps", "application/vnd.ms-works");
            MimeMapping.AddMimeMapping(".wav", "audio/wav");
            MimeMapping.AddMimeMapping(".wrz", "x-world/x-vrml");
            MimeMapping.AddMimeMapping(".wri", "application/x-mswrite");
            MimeMapping.AddMimeMapping(".wks", "application/vnd.ms-works");
            MimeMapping.AddMimeMapping(".wmf", "application/x-msmetafile");
            MimeMapping.AddMimeMapping(".wcm", "application/vnd.ms-works");
            MimeMapping.AddMimeMapping(".wrl", "x-world/x-vrml");
            MimeMapping.AddMimeMapping(".wdb", "application/vnd.ms-works");
            MimeMapping.AddMimeMapping(".wsdl", "text/xml");
            MimeMapping.AddMimeMapping(".xap", "application/x-silverlight-app");
            MimeMapping.AddMimeMapping(".xml", "text/xml");
            MimeMapping.AddMimeMapping(".xlm", "application/vnd.ms-excel");
            MimeMapping.AddMimeMapping(".xaf", "x-world/x-vrml");
            MimeMapping.AddMimeMapping(".xla", "application/vnd.ms-excel");
            MimeMapping.AddMimeMapping(".xls", "application/vnd.ms-excel");
            MimeMapping.AddMimeMapping(".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            MimeMapping.AddMimeMapping(".xof", "x-world/x-vrml");
            MimeMapping.AddMimeMapping(".xlt", "application/vnd.ms-excel");
            MimeMapping.AddMimeMapping(".xlc", "application/vnd.ms-excel");
            MimeMapping.AddMimeMapping(".xsl", "text/xml");
            MimeMapping.AddMimeMapping(".xbm", "image/x-xbitmap");
            MimeMapping.AddMimeMapping(".xlw", "application/vnd.ms-excel");
            MimeMapping.AddMimeMapping(".xpm", "image/x-xpixmap");
            MimeMapping.AddMimeMapping(".xwd", "image/x-xwindowdump");
            MimeMapping.AddMimeMapping(".xsd", "text/xml");
            MimeMapping.AddMimeMapping(".z", "application/x-compress");
            MimeMapping.AddMimeMapping(".zip", "application/x-zip-compressed");
            MimeMapping.AddMimeMapping(".*", "application/octet-stream");
        }
    }
            
}
