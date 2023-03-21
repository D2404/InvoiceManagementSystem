using InvoiceManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoiceManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        clsCommon objCommon = new clsCommon();
        public ActionResult Index(DashboardModel cls)
        {
            try
            {
                if (objCommon.getUserIdFromSession() != 0)
                {
                    cls = cls.GetUserAccountDashboardCount(cls);
                    cls.LSTTeacherList = cls.GetTeacherDetailsList(cls);
                    cls.LSTStudentList = cls.GetStudentDetailsList(cls);
                    return View(cls);
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Index1()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult GetClassRoom(ClassRoomModel cls)
        {
            try
            {
                int TotalEntries = 0;
                int showingEntries = 0;
                int startentries = 0;
                List<ClassRoomModel> lstClassRoomList = new List<ClassRoomModel>();
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_ClassRoomList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);
                conn.Close();


                if (dt != null && dt.Rows.Count > 0)
                {

                    for (var i = 0; i < dt.Rows.Count; i++)
                    {
                        ClassRoomModel obj = new ClassRoomModel();
                        obj.Id = Convert.ToInt32(dt.Rows[i]["Id"] == null || dt.Rows[i]["Id"].ToString().Trim() == "" ? null : dt.Rows[i]["Id"].ToString());
                        obj.IsActive = Convert.ToBoolean(dt.Rows[i]["IsActive"] == null || dt.Rows[i]["IsActive"].ToString().Trim() == "" ? null : dt.Rows[i]["IsActive"].ToString());
                        obj.ClassNo = dt.Rows[i]["ClassNo"] == null || dt.Rows[i]["ClassNo"].ToString().Trim() == "" ? null : dt.Rows[i]["ClassNo"].ToString();
                        //obj.ROWNUMBER = Convert.ToInt32(dt.Rows[i]["ROWNUMBER"] == null || dt.Rows[i]["ROWNUMBER"].ToString().Trim() == "" ? null : dt.Rows[i]["ROWNUMBER"].ToString());
                        lstClassRoomList.Add(obj);
                    }
                }
                cls.LSTClassRoomList = lstClassRoomList;

                return PartialView("_ClassRoomList", cls);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}