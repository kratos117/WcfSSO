using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using System.Web.Security;
using System.Security.Cryptography;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.Configuration;

namespace WcfSSO
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“Service1”。
    public class Service1 : IService1
    {
        /// <summary>
        /// 判断用户密码是否存在
        /// </summary>
        /// <param name="composite"></param>
        /// <returns></returns>
        private TEMPLOYEEINFO IsExist(CompositeType composite)
        {
            using (physical_SSOEntities SSOE = new physical_SSOEntities()) //调用实体数据库
            {
                var EMPLOYEEINFO = from employee in SSOE.TEMPLOYEEINFO
                          where employee.FEMPLOYEE == composite.Employee && employee.FPASSWORD == composite.Pwd
                          select employee;

                foreach (var i in EMPLOYEEINFO)
                {
                    return i;
                }
                return null;
            }
        }

        /// <summary>
        /// 判断用户是否有登录系统的权限
        /// </summary>
        /// <param name="composite"></param>
        /// <returns></returns>
        private TSYSTEMCONTROL IsControl(CompositeType composite)
        {
            using (physical_SSOEntities SSOE = new physical_SSOEntities()) //调用实体数据库
            {
                var SYSTEMCONTROL = from systemcontrol in SSOE.TSYSTEMCONTROL
                                   where systemcontrol.FEMPLOYEE == composite.Employee && systemcontrol.FSYSTEM == composite.System
                                   select systemcontrol;

                foreach (var i in SYSTEMCONTROL)
                {
                    return i;
                }
                return null;
            }
        }

        /// <summary>
        /// 判断IP是否存在
        /// </summary>
        /// <param name="composite"></param>
        /// <returns></returns>
        private TIPLIST IsIPExist(CompositeType composite)
        {
            using (physical_SSOEntities SSOE = new physical_SSOEntities()) //调用实体数据库
            {
                var IPLIST = from iplist in SSOE.TIPLIST
                                    where iplist.FIP == composite.IP
                                    select iplist;

                foreach (var i in IPLIST)
                {
                    return i;
                }
                return null;
            }
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="composite"></param>
        /// <returns></returns>
        public CompositeType IsLogin(CompositeType composite)
        {
            if (IsIPExist(composite) != null)   //判断ip
            {
                composite.Pwd = MD5Encrypt(composite.Pwd);
                TEMPLOYEEINFO employee = IsExist(composite);    //判断用户名密码是否正确
                if (employee != null)
                {
                    if (employee.FSTATUS == 1)
                    {
                        composite.Value = "此员工已离职!";
                    }
                    else
                    {
                        composite.Group = employee.FGROUP;

                        if (IsControl(composite) != null)   //判断登录系统权限
                        {
                            composite.Value = "成功";
                            composite.RoleID = employee.FROLEID;
                            composite.IsAccess = true;
                        }
                        else
                        {
                            composite.Value = "此账号没有登陆此系统的权限!";
                        }
                    }
                }
                else
                {
                    composite.Value = "账号或密码错误!";
                }
            }
            else
            {
                composite.Value = "此IP无法访问!";
            }
            return composite;
        }

        private string MD5Encrypt(string strPwd)
        {
            //MD5 md5 = new MD5CryptoServiceProvider();
            //byte[] result = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(strPwd));
            //return System.Text.Encoding.Default.GetString(result);

            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] md5data = md5.ComputeHash(System.Text.UTF8Encoding.Default.GetBytes(strPwd));
            md5.Clear();
            return Convert.ToBase64String(md5data);
        }

        private void UpdatePwd()
        {
            DataSet ds = dbHelp.GetQuery("select FEMPLOYEE,FPASSWORD,FSECOND_PWD from TEMPLOYEEINFO");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dbHelp.GetExecuteSql("update TEMPLOYEEINFO set FPASSWORD='" + MD5Encrypt(ds.Tables[0].Rows[i]["FPASSWORD"].ToString())
                    + "',FSECOND_PWD='" + MD5Encrypt(ds.Tables[0].Rows[i]["FSECOND_PWD"].ToString()) + "' where FEMPLOYEE='" + ds.Tables[0].Rows[i]["FEMPLOYEE"].ToString() + "'");
            }
        }
    }
}
