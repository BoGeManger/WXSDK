using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Net;
using System.ComponentModel;
using System.IO;

namespace WXSDK.Models
{
    #region 输出返回信息对象
    public class ResultEntity<T>
    {
        public ResultEntity()
        {
            this.ResultCode = 500;
        }
        /// <summary>
        /// ResultMsg
        /// </summary>
        public string ResultMsg { get; set; }
        /// <summary>
        /// ResultCode(成功：200;失败:500)
        /// </summary>
        public int ResultCode { get; set; }
        /// <summary>
        /// ResultObject
        /// </summary>
        public T ResultObject { get; set; }
    }
    #endregion

    #region 获取登录二维码图片
    public class OutGetLoginQRCode
    {
        /// <summary>
        /// 二维码图片
        /// </summary>
        public Image img { get; set; }
        /// <summary>
        /// 图片编码
        /// </summary>
        public string qruuid { get; set; }
        /// <summary>
        /// cookie
        /// </summary>
        public CookieCollection _CookieCollection { get; set; }
    }
    #endregion

    #region 检查二维码扫描状态
    public class OutWaitForLoginEntity
    {
        /// <summary>
        /// 登录成功后获取到的重定向地址，使用一次
        /// </summary>
        public string redirect_uri { get; set; }
        /// <summary>
        /// cookie
        /// </summary>
        public CookieCollection _CookieCollection { get; set; }
    }
    #endregion

    #region 用户登录
    //Uin,Sid,Skey,Pass_ticket.微信认证标志，认证完毕获取，以后交互时要使用
    public class OutLoginEntity
    {
        public string Pass_ticket { get; set; }
        public string Skey { get; set; }
        public string Sid { get; set; }
        public string Uin { get; set; }
        public BaseRequest BaseRequest { get; set; }
        public CookieCollection _CookieCollection { get; set; }
    }

    //当前验证数据字典，大多数post需要该字段
    public class BaseRequest
    {
        public string Skey { get; set; }
        public string Sid { get; set; }
        public string Uin { get; set; }
        public string DeviceID { get; set; }
    }
    #endregion

    #region 微信初始化
    public class OutInitEntity
    {
        public UserEntity UserEntity { get; set; }

        public SyncKeyEntity SyncKeyEntity { get; set; }
        /// <summary>
        /// cookie
        /// </summary>
        public CookieCollection _CookieCollection { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<MemberObjEntity> ChatGroupLstEntity { get; set; }
    }

    /// <summary>
    ///个人信息
    /// </summary>
    public class UserEntity
    {
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
        /// HeadImgUrl
        /// </summary>
        public string HeadImgUrl { get; set; }
        /// <summary>
        /// HeadImg
        /// </summary>
        public Image HeadImg { get; set; }
        /// <summary>
        /// RemarkName
        /// </summary>
        public string RemarkName { get; set; }
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
        /// HideInputBarFlag
        /// </summary>
        public long HideInputBarFlag { get; set; }
        /// <summary>
        /// StarFriend
        /// </summary>
        public long StarFriend { get; set; }
        /// <summary>
        /// Sex
        /// </summary>
        public long Sex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Signature { get; set; }
        /// <summary>
        /// AppAccountFlag
        /// </summary>
        public long AppAccountFlag { get; set; }
        /// <summary>
        /// VerifyFlag
        /// </summary>
        public long VerifyFlag { get; set; }
        /// <summary>
        /// ContactFlag
        /// </summary>
        public long ContactFlag { get; set; }
        /// <summary>
        /// WebWxPluginSwitch
        /// </summary>
        public long WebWxPluginSwitch { get; set; }
        /// <summary>
        /// HeadImgFlag
        /// </summary>
        public long HeadImgFlag { get; set; }
        /// <summary>
        /// SnsFlag
        /// </summary>
        public long SnsFlag { get; set; }
    }

    public class SyncKeyListEntity
    {
        /// <summary>
        /// Key
        /// </summary>
        public long Key { get; set; }
        /// <summary>
        /// Val
        /// </summary>
        public long Val { get; set; }
    }

    public class SyncKeyEntity
    {
        /// <summary>
        /// Count
        /// </summary>
        public long Count { get; set; }
        /// <summary>
        /// List
        /// </summary>
        public List<SyncKeyListEntity> List { get; set; }
    }
    #endregion

    #region 开启微信状态通知
    public class OutStatusNotifyEntity
    {
        /// <summary>
        /// 状态（成功:true;失败false）
        /// </summary>
        public bool status { get; set; }
        /// <summary>
        /// cookie
        /// </summary>
        public CookieCollection _CookieCollection { get; set; }
    }
    #endregion

    #region 获取用户联系人(普通成员，公众号，群聊，其他)
    public class OutGetContactEntity
    {
        /// <summary>
        /// 群聊
        /// </summary>
        public List<MemberObjEntity> ChatGroupLstEntity { get; set; }
        /// <summary>
        /// 普通成员
        /// </summary>
        public List<MemberObjEntity> MemUserLstEntity { get; set; }
        /// <summary>
        /// 公众号
        /// </summary>
        public List<MemberObjEntity> MemberGroupLstEntity { get; set; }
        /// <summary>
        /// 其他
        /// </summary>
        public List<MemberObjEntity> OtherMemLstEntity { get; set; }
    }

    /// <summary>
    /// 全部信息
    /// </summary>
    public class MemberObjEntity
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
        /// UserName
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// NickName
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// HeadImgUrl
        /// </summary>
        public string HeadImgUrl { get; set; }
        /// <summary>
        /// HeadImg
        /// </summary>
        public Image HeadImg { get; set; }
        /// <summary>
        /// ContactFlag 1
        /// </summary>
        public long ContactFlag { get; set; }
        /// <summary>
        /// MemberCount 0
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
        /// HideInputBarFlag 0
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
        /// 省份
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 城市
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

    #region MemberBaseEntity
    public class MemberBaseEntity
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
    #endregion

    #endregion

    #region 获取服务器当前产生的消息
    public class OutSyncEntity
    {
        /// <summary>
        /// 消息数量
        /// </summary>
        public long AddMsgCount { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public List<AddMsgList> AddMsgList { get; set; }
        /// <summary>
        /// 变更联系人数量
        /// </summary>
        public long ModContactCount { get; set; }
        /// <summary>
        /// 变更的联系人信息
        /// </summary>
        public List<ModContactList> ModContactList { get; set; }
        /// <summary>
        /// SyncKey
        /// </summary>
        public SyncKeyEntity SyncKey { get; set; }
        /// <summary>
        /// 最近联系人
        /// </summary>
        public List<string> LateUserList { get; set; }
    }

    public class ModContactList
    {
        /// <summary>
        /// UserName
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// NickName
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// Sex
        /// </summary>
        public long Sex { get; set; }
        /// <summary>
        /// HeadImgUpdateFlag
        /// </summary>
        public long HeadImgUpdateFlag { get; set; }
        /// <summary>
        /// ContactType
        /// </summary>
        public long ContactType { get; set; }
        /// <summary>
        /// Alias
        /// </summary>
        public string Alias { get; set; }
        /// <summary>
        /// ChatRoomOwner
        /// </summary>
        public string ChatRoomOwner { get; set; }
        /// <summary>
        /// HeadImgUrl
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
        /// HideInputBarFlag
        /// </summary>
        public long HideInputBarFlag { get; set; }
        /// <summary>
        /// Signature
        /// </summary>
        public string Signature { get; set; }
        /// <summary>
        /// VerifyFlag
        /// </summary>
        public long VerifyFlag { get; set; }
        /// <summary>
        /// RemarkName
        /// </summary>
        public string RemarkName { get; set; }
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
        /// SnsFlag
        /// </summary>
        public long SnsFlag { get; set; }
        /// <summary>
        /// KeyWord
        /// </summary>
        public string KeyWord { get; set; }
    }

    public class AddMsgList
    {
        /// <summary>
        /// 消息ID
        /// </summary>
        public string MsgId { get; set; }
        /// <summary>
        /// 发送用户
        /// </summary>
        public string FromUserName { get; set; }
        /// <summary>
        /// 群内消息发送用户
        /// </summary>
        public string FromGroupUser { get; set; }
        /// <summary>
        /// 接收用户
        /// </summary>
        public string ToUserName { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public int MsgType { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        public long Status { get; set; }
        /// <summary>
        /// ImgStatus
        /// </summary>
        public long ImgStatus { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        public long CreateTime { get; set; }
        /// <summary>
        /// VoiceLength
        /// </summary>
        public long VoiceLength { get; set; }
        /// <summary>
        /// PlayLength
        /// </summary>
        public long PlayLength { get; set; }
        /// <summary>
        /// 文件，语音
        /// </summary>
        public byte[] File { get; set; }
        /// <summary>
        /// FileName
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// FileSize
        /// </summary>
        public string FileSize { get; set; }
        /// <summary>
        /// 媒体ID
        /// </summary>
        public string MediaId { get; set; }
        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// AppMsgType
        /// </summary>
        public long AppMsgType { get; set; }
        /// <summary>
        /// StatusNotifyCode
        /// </summary>
        public long StatusNotifyCode { get; set; }
        /// <summary>
        /// StatusNotifyUserName
        /// </summary>
        public string StatusNotifyUserName { get; set; }
        /// <summary>
        /// RecommendInfo
        /// </summary>
        public RecommendInfo RecommendInfo { get; set; }
        /// <summary>
        /// ForwardFlag
        /// </summary>
        public long ForwardFlag { get; set; }
        /// <summary>
        /// AppInfo
        /// </summary>
        public AppInfo AppInfo { get; set; }
        /// <summary>
        /// HasProductId
        /// </summary>
        public long HasProductId { get; set; }
        /// <summary>
        /// Ticket
        /// </summary>
        public string Ticket { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public Image Img { get; set; }
        /// <summary>
        /// 图片高度
        /// </summary>
        public long ImgHeight { get; set; }
        /// <summary>
        /// 图片宽度
        /// </summary>
        public long ImgWidth { get; set; }
        /// <summary>
        /// SubMsgType
        /// </summary>
        public long SubMsgType { get; set; }
        /// <summary>
        /// NewMsgId
        /// </summary>
        public long NewMsgId { get; set; }
        /// <summary>
        /// OriContent
        /// </summary>
        public string OriContent { get; set; }
    }

    public class RecommendInfo
    {
        /// <summary>
        /// UserName
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// NickName
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// QQNum
        /// </summary>
        public long QQNum { get; set; }
        /// <summary>
        /// Province
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// City
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Content
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// Signature
        /// </summary>
        public string Signature { get; set; }
        /// <summary>
        /// Alias
        /// </summary>
        public string Alias { get; set; }
        /// <summary>
        /// Scene
        /// </summary>
        public long Scene { get; set; }
        /// <summary>
        /// VerifyFlag
        /// </summary>
        public long VerifyFlag { get; set; }
        /// <summary>
        /// AttrStatus
        /// </summary>
        public long AttrStatus { get; set; }
        /// <summary>
        /// Sex
        /// </summary>
        public long Sex { get; set; }
        /// <summary>
        /// Ticket
        /// </summary>
        public string Ticket { get; set; }
        /// <summary>
        /// OpCode
        /// </summary>
        public long OpCode { get; set; }
    }

    public class AppInfo
    {
        /// <summary>
        /// AppID
        /// </summary>
        public string AppID { get; set; }
        /// <summary>
        /// Type
        /// </summary>
        public long Type { get; set; }
    }
    #endregion

    #region 发送消息
    public class OutSendMsgEntity
    {
        public string MsgID { get; set; }

        public string LocalID { get; set; }

        public OutUploadMediaEntity _OutUploadMediaEntity { get; set; }
    }
    #endregion

    #region 添加好友，微信公众号关注,验证通过
    public class VerifyUserList
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 添加好友后返回的ticket信息，添加操作保持值为空
        /// </summary>
        public string VerifyUserTicket { get; set; }
    }
    #endregion

    #region 附件上传
    public class UploadMediaRequest
    {
        /// <summary>
        ///UploadType
        /// </summary>
        public int UploadType { get; set; }
        /// <summary>
        /// BaseRequest
        /// </summary>
        public BaseRequest BaseRequest { get; set; }
        /// <summary>
        /// ClientMediaId
        /// </summary>
        public Int64 ClientMediaId { get; set; }
        /// <summary>
        /// TotalLen
        /// </summary>
        public long TotalLen { get; set; }
        /// <summary>
        /// StartPos
        /// </summary>
        public int StartPos { get; set; }
        /// <summary>
        /// DataLen
        /// </summary>
        public long DataLen { get; set; }
        /// <summary>
        /// MediaType
        /// </summary>
        public int MediaType { get; set; }
        /// <summary>
        /// FromUserName
        /// </summary>
        public string FromUserName { get; set; }
        /// <summary>
        /// ToUserName
        /// </summary>
        public string ToUserName { get; set; }
        /// <summary>
        /// FileMd5
        /// </summary>
        public string FileMd5 { get; set; }
    }

    public class OutUploadMediaEntity
    {
        public string MediaId { get; set; }
        public int StartPos { get; set; }
        public int CDNThumbImgHeight { get; set; }
        public int CDNThumbImgWidth { get; set; }
        public string EncryFileName { get; set; }
    }
    #endregion

    #region 消息处理
    public enum MsgTypeEnum : int
    {
        /// <summary>
        /// 文本消息
        /// </summary>
        [Description("文本消息")]
        Text = 1,
        /// <summary>
        /// 图片消息
        /// </summary>
        [Description("图片消息")]
        Img = 3,
        /// <summary>
        /// 语音消息
        /// </summary>
        [Description("语音消息")]
        Voice = 34,
        /// <summary>
        /// 好友验证消息
        /// </summary>
        [Description("好友验证消息")]
        Verify = 37,
        /// <summary>
        /// 通过好友验证消息
        /// </summary>
        [Description("通过好友验证消息")]
        PossibleFriend = 40,
        /// <summary>
        /// 共享名片
        /// </summary>
        [Description("共享名片")]
        ShareCard = 42,
        /// <summary>
        /// 视频通话消息
        /// </summary>
        [Description("视频通话消息")]
        VideoCall = 43,
        /// <summary>
        /// 动画表情
        /// </summary>
        [Description("动画表情")]
        AnimBrow = 47,
        /// <summary>
        /// 位置消息
        /// </summary>
        [Description("位置消息")]
        Position = 48,
        /// <summary>
        /// 分享链接
        /// </summary>
        [Description("分享链接")]
        ShareUrl = 49,
        /// <summary>
        /// VOIP
        /// </summary>
        [Description("VOIP")]
        VOIP = 50,
        /// <summary>
        /// 微信初始化消息
        /// </summary>
        [Description("微信初始化消息")]
        Init = 51,
        /// <summary>
        /// VOIPNOTIFY
        /// </summary>
        [Description("VOIPNOTIFY")]
        VOIPNOTIFY = 52,
        /// <summary>
        /// VOIPINVITE
        /// </summary>
        [Description("VOIPINVITE")]
        VOIPINVITE = 53,
        /// <summary>
        /// 小视频
        /// </summary>
        [Description("小视频")]
        Video = 62,
        /// <summary>
        /// SYSNOTICE
        /// </summary>
        [Description("SYSNOTICE")]
        SYSNOTICE = 9999,
        /// <summary>
        /// 系统消息
        /// </summary>
        [Description("系统消息")]
        System = 10000,
        /// <summary>
        /// 撤回消息
        /// </summary>
        [Description("撤回消息")]
        Revoke = 10002,
    }
    #endregion
}
