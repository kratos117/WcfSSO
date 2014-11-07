using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfSSO
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“ISrvEmployeeInfo”。
    [ServiceContract]
    public interface ISrvEmployeeInfo
    {
        [OperationContract]
        List<TEmployeeInfo> QueryConsultantList(string fcenter);

        [OperationContract]
        List<TEmployeeInfo> QueryTherapistList(string fcenter);

        [OperationContract]
        List<TUserGroup> GetGroups();

        [OperationContract]
        PaginatorEmployee QueryEmployees(TEmployeeInfo data, string Findate1, string Findate2, int pageIndex, int pageSize);
    }

    #region TEmployeeInfo
    [DataContract]
    public class TEmployeeInfo
    {
        private string _FEMPLOYEE;
        [DataMember]
        public string FEMPLOYEE
        {
            get { return _FEMPLOYEE; }
            set { _FEMPLOYEE = value; }
        }
        private string _FID;
        [DataMember]
        public string FID
        {
            get { return _FID; }
            set { _FID = value; }
        }
        private string _FNAME;
        [DataMember]
        public string FNAME
        {
            get { return _FNAME; }
            set { _FNAME = value; }
        }
        private string _FSEX;
        [DataMember]
        public string FSEX
        {
            get { return _FSEX; }
            set { _FSEX = value; }
        }
        private string _FCONTACT;
        [DataMember]
        public string FCONTACT
        {
            get { return _FCONTACT; }
            set { _FCONTACT = value; }
        }
        private string _FEMAIL;

        [DataMember]
        public string FEMAIL
        {
            get { return _FEMAIL; }
            set { _FEMAIL = value; }
        }
        private string _FPOPUP;

        [DataMember]
        public string FPOPUP
        {
            get { return _FPOPUP; }
            set { _FPOPUP = value; }
        }
        private string _FCHKINMSG;

        [DataMember]
        public string FCHKINMSG
        {
            get { return _FCHKINMSG; }
            set { _FCHKINMSG = value; }
        }
        private string _FLASTACCESS;

        [DataMember]
        public string FLASTACCESS
        {
            get { return _FLASTACCESS; }
            set { _FLASTACCESS = value; }
        }
        private int _FSTATUS;

        [DataMember]
        public int FSTATUS
        {
            get { return _FSTATUS; }
            set { _FSTATUS = value; }
        }
        private int _FUSERLEVEL;

        [DataMember]
        public int FUSERLEVEL
        {
            get { return _FUSERLEVEL; }
            set { _FUSERLEVEL = value; }
        }
        private string _FPASSWORD;

        [DataMember]
        public string FPASSWORD
        {
            get { return _FPASSWORD; }
            set { _FPASSWORD = value; }
        }
        private string _FSECOND_PWD;

        [DataMember]
        public string FSECOND_PWD
        {
            get { return _FSECOND_PWD; }
            set { _FSECOND_PWD = value; }
        }
        private string _FGROUP;

        [DataMember]
        public string FGROUP
        {
            get { return _FGROUP; }
            set { _FGROUP = value; }
        }

        private int _FROLEID;
        [DataMember]
        public int FROLEID
        {
            get { return _FROLEID; }
            set { _FROLEID = value; }
        }
        private int _ISUSER;

        [DataMember]
        public int ISUSER
        {
            get { return _ISUSER; }
            set { _ISUSER = value; }
        }
        private int _FATRAINER;

        [DataMember]
        public int FATRAINER
        {
            get { return _FATRAINER; }
            set { _FATRAINER = value; }
        }
        private int _FSTRAINER;

        [DataMember]
        public int FSTRAINER
        {
            get { return _FSTRAINER; }
            set { _FSTRAINER = value; }
        }
        private int _FWTRAINER;

        [DataMember]
        public int FWTRAINER
        {
            get { return _FWTRAINER; }
            set { _FWTRAINER = value; }
        }
        private string _FSNAME;

        [DataMember]
        public string FSNAME
        {
            get { return _FSNAME; }
            set { _FSNAME = value; }
        }
        private string _FCENTER;

        [DataMember]
        public string FCENTER
        {
            get { return _FCENTER; }
            set { _FCENTER = value; }
        }
        private string _FCOMMTYPE;

        [DataMember]
        public string FCOMMTYPE
        {
            get { return _FCOMMTYPE; }
            set { _FCOMMTYPE = value; }
        }
        private string _FBLISTREMARK;

        [DataMember]
        public string FBLISTREMARK
        {
            get { return _FBLISTREMARK; }
            set { _FBLISTREMARK = value; }
        }
        private string _FBLISTDATE;

        [DataMember]
        public string FBLISTDATE
        {
            get { return _FBLISTDATE; }
            set { _FBLISTDATE = value; }
        }
        private string _FPREPAREDBY;

        [DataMember]
        public string FPREPAREDBY
        {
            get { return _FPREPAREDBY; }
            set { _FPREPAREDBY = value; }
        }
        private string _FINDATE;

        [DataMember]
        public string FINDATE
        {
            get { return _FINDATE; }
            set { _FINDATE = value; }
        }
        private string _FOUTDATE;

        [DataMember]
        public string FOUTDATE
        {
            get { return _FOUTDATE; }
            set { _FOUTDATE = value; }
        }
        private string _FPTPLAN;

        [DataMember]
        public string FPTPLAN
        {
            get { return _FPTPLAN; }
            set { _FPTPLAN = value; }
        }
        private byte[] _FIMAGE;

        [DataMember]
        public byte[] FIMAGE
        {
            get { return _FIMAGE; }
            set { _FIMAGE = value; }
        }
        private int _FCHECKOUTED;

        [DataMember]
        public int FCHECKOUTED
        {
            get { return _FCHECKOUTED; }
            set { _FCHECKOUTED = value; }
        }
        private string _FTYPE;

        [DataMember]
        public string FTYPE
        {
            get { return _FTYPE; }
            set { _FTYPE = value; }
        }
        private string _FANNUAL_LEAVE;

        [DataMember]
        public string FANNUAL_LEAVE
        {
            get { return _FANNUAL_LEAVE; }
            set { _FANNUAL_LEAVE = value; }
        }
        private string _FFULL_TIME;

        [DataMember]
        public string FFULL_TIME
        {
            get { return _FFULL_TIME; }
            set { _FFULL_TIME = value; }
        }
        private string _SHIFT;

        [DataMember]
        public string SHIFT
        {
            get { return _SHIFT; }
            set { _SHIFT = value; }
        }
    }
    #endregion

    #region TUserGroup
    [DataContract]
    public class TUserGroup
    {
        private string _FCODE = "";
        private string _FDESC = "";
        private decimal _FPTGROUP = 0;
        [DataMember]
        public string FCODE
        {
            get { return _FCODE; }
            set { _FCODE = value; }
        }
        [DataMember]
        public string FDESC
        {
            get { return _FDESC; }
            set { _FDESC = value; }
        }
        [DataMember]
        public decimal FPTGROUP
        {
            get { return _FPTGROUP; }
            set { _FPTGROUP = value; }
        }
    }
    #endregion

    [DataContract]
    public class PaginatorEmployee
    {
        private int rcount = 0;
        private int pcount = 0;
        private int pageIndex = 0;
        private int pageSize = 0;
        private List<TEmployeeInfo> list = new List<TEmployeeInfo>();
        [DataMember]
        public int Rcount
        {
            get { return rcount; }
            set { rcount = value; }
        }
        [DataMember]
        public int Pcount
        {
            get { return pcount; }
            set { pcount = value; }
        }
        [DataMember]
        public int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value; }
        }
        [DataMember]
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }
        [DataMember]
        public List<TEmployeeInfo> List
        {
            get { return list; }
            set { list = value; }
        }
    }
}
