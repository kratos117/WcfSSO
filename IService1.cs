using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfSSO
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        CompositeType IsLogin(CompositeType composite);
    }

    // 使用下面示例中说明的数据约定将复合类型添加到服务操作。
    [DataContract]
    public class CompositeType
    {
        string strEmployee;
        string strPwd;
        string strSystem;
        string strValue;
        string strGroup;
        string strIp;

        /// <summary>
        /// 账号
        /// </summary>
        [DataMember]
        public string Employee
        {
            get { return strEmployee; }
            set { strEmployee = value; }
        }

        /// <summary>
        /// 密码
        /// </summary>
        [DataMember]
        public string Pwd
        {
            get { return strPwd; }
            set { strPwd = value; }
        }

        /// <summary>
        /// 系统
        /// </summary>
        [DataMember]
        public string System
        {
            get { return strSystem; }
            set { strSystem = value; }
        }

        /// <summary>
        /// 结果
        /// </summary>
        [DataMember]
        public string Value
        {
            get { return strValue; }
            set { strValue = value; }
        }

        /// <summary>
        /// 组（职位）
        /// </summary>
        [DataMember]
        public string Group
        {
            get { return strGroup; }
            set { strGroup = value; }
        }

        /// <summary>
        /// IP
        /// </summary>
        [DataMember]
        public string IP
        {
            get { return strIp; }
            set { strIp = value; }
        }

        private bool _isAccess = false;
        [DataMember]
        public bool IsAccess
        {
            get { return _isAccess; }
            set { _isAccess = value; }
        }
        private int _roleID = 0;
        [DataMember]
        public int RoleID
        {
            get { return _roleID; }
            set { _roleID = value; }
        }
    }
}
