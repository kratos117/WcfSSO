using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.Objects;

namespace WcfSSO
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“SrvEmployeeInfo”。
    public class SrvEmployeeInfo : ISrvEmployeeInfo
    {
        /// <summary>
        /// 查询分店里的所有美容顾问
        /// </summary>
        /// <param name="fcenter"></param>
        /// <returns></returns>
        public List<TEmployeeInfo> QueryConsultantList(string fcenter)
        {
            List<TEmployeeInfo> list = new List<TEmployeeInfo>();
            using (physical_SSOEntities SSOE = new physical_SSOEntities()) //调用实体数据库
            {
                var EMPLOYEEINFO = from employee in SSOE.TEMPLOYEEINFO
                                   where employee.FCENTER == fcenter && employee.FGROUP == "CONSULTANT"
                                   select employee;

                foreach (var i in EMPLOYEEINFO)
                {
                    TEmployeeInfo t = new TEmployeeInfo();
                    t.FEMPLOYEE = i.FEMPLOYEE;
                    t.FNAME = i.FNAME;
                    list.Add(t);
                }
                return list;
            }
        }

        /// <summary>
        /// 查询分店里的所有美容理疗师
        /// </summary>
        /// <param name="fcenter"></param>
        /// <returns></returns>
        public List<TEmployeeInfo> QueryTherapistList(string fcenter)
        {
            List<TEmployeeInfo> list = new List<TEmployeeInfo>();
            using (physical_SSOEntities SSOE = new physical_SSOEntities()) //调用实体数据库
            {
                var EMPLOYEEINFO = from employee in SSOE.TEMPLOYEEINFO
                                   where employee.FCENTER == fcenter && employee.FGROUP == "THERAPIST"
                                   select employee;

                foreach (var i in EMPLOYEEINFO)
                {
                    TEmployeeInfo t = new TEmployeeInfo();
                    t.FEMPLOYEE = i.FEMPLOYEE;
                    t.FNAME = i.FNAME;
                    list.Add(t);
                }
                return list;
            }
        }

        public List<TUserGroup> GetGroups()
        {
            List<TUserGroup> list = new List<TUserGroup>();
            using (physical_SSOEntities e = new physical_SSOEntities())
            {
                try
                {
                    var r = (from t in e.TUSERGROUP select t);
                    if (null != r)
                    {
                        foreach (var i in r)
                        {
                            TUserGroup d = new TUserGroup();
                            d.FCODE = i.FCODE;
                            d.FDESC = i.FDESC;
                            d.FPTGROUP = i.FPTGROUP;
                            list.Add(d);
                        }
                    }
                }
                catch (Exception ex) { throw ex; }
            }
            return list;
        }

        public PaginatorEmployee QueryEmployees(TEmployeeInfo data, string Findate1, string Findate2, int pageIndex, int pageSize)
        {
            PaginatorEmployee ret = new PaginatorEmployee();
            using (physical_SSOEntities e = new physical_SSOEntities())
            {
                try
                {
                    ObjectParameter pCount = new ObjectParameter("PCount", typeof(int));//总页数输出
                    ObjectParameter rCount = new ObjectParameter("RCount", typeof(int));//总记录数输出
                    e.proc_employee_paginator_count(pCount, rCount,
                        data.FEMPLOYEE, data.FNAME, data.FID,
                        data.FCONTACT, data.FGROUP, data.FROLEID, data.FSTATUS,
                        Findate1, Findate2, 0, pageIndex, pageSize);
                    if (null == rCount || null == pCount) { return ret; }

                    ret.Pcount = Int32.Parse(pCount.Value.ToString());
                    ret.Rcount = Int32.Parse(rCount.Value.ToString());
                    ret.PageIndex = pageIndex;
                    ret.PageSize = pageSize;

                    ObjectResult<TEMPLOYEEINFO> or = e.proc_employee_paginator(pCount, rCount,
                        data.FEMPLOYEE, data.FNAME, data.FID,
                        data.FCONTACT, data.FGROUP, data.FROLEID, data.FSTATUS,
                        Findate1, Findate2, 0, pageIndex, pageSize);
                    List<TEMPLOYEEINFO> t = or.ToList<TEMPLOYEEINFO>();
                    foreach (var i in t)
                    {
                        TEmployeeInfo d = new TEmployeeInfo();
                        d.FEMPLOYEE = i.FEMPLOYEE;
                        d.FID = i.FID;
                        d.FNAME = i.FNAME;
                        d.FSEX = i.FSEX;
                        d.FCONTACT = i.FCONTACT;
                        d.FEMAIL = i.FEMAIL;
                        d.FPOPUP = i.FPOPUP;
                        d.FCHKINMSG = i.FCHKINMSG;
                        d.FLASTACCESS = i.FLASTACCESS;
                        d.FSTATUS = i.FSTATUS.Value;
                        d.FUSERLEVEL = i.FUSERLEVEL.Value;
                        d.FPASSWORD = i.FPASSWORD;
                        d.FSECOND_PWD = i.FSECOND_PWD;
                        d.FGROUP = i.FGROUP;
                        d.FROLEID = i.FROLEID;
                        d.ISUSER = i.ISUSER.Value;
                        d.FATRAINER = i.FATRAINER.Value;
                        d.FSTRAINER = i.FSTRAINER.Value;
                        d.FWTRAINER = i.FWTRAINER.Value;
                        d.FSNAME = i.FSNAME;
                        d.FCENTER = i.FCENTER;
                        d.FCOMMTYPE = i.FCOMMTYPE;
                        d.FBLISTREMARK = i.FBLISTREMARK;
                        if (1 == i.FBLISTDATE.Value.Year)
                        {
                            d.FBLISTDATE = string.Empty;
                        }
                        else
                        {
                            d.FBLISTDATE = i.FBLISTDATE.Value.ToString("yyyy-MM-dd");
                        }
                        d.FPREPAREDBY = i.FPREPAREDBY;
                        if (1 == i.FINDATE.Value.Year)
                        {
                            d.FINDATE = string.Empty;
                        }
                        else
                        {
                            d.FINDATE = i.FINDATE.Value.ToString("yyyy-MM-dd");
                        }
                        if (1 == i.FOUTDATE.Value.Year)
                        {
                            d.FOUTDATE = string.Empty;
                        }
                        else
                        {
                            d.FOUTDATE = i.FOUTDATE.Value.ToString("yyyy-MM-dd");
                        }
                        d.FPTPLAN = i.FPTPLAN;
                        d.FIMAGE = i.FIMAGE;
                        d.FCHECKOUTED = i.FCHECKOUTED.Value;
                        d.FTYPE = i.FTYPE;
                        d.FANNUAL_LEAVE = i.FANNUAL_LEAVE;
                        d.FFULL_TIME = i.FFULL_TIME;
                        d.SHIFT = i.SHIFT;
                        ret.List.Add(d);
                    }
                }
                catch (Exception ex) { throw ex; }
                return ret;
            }
        }
    }
}
