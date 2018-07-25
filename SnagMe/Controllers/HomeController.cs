using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SnagMe.Models;
using SnagMe.Entities;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace SnagMe.Controllers
{
    public class HomeController : Controller
    {
        public List<string> Applicants { get; set; }

        public ActionResult Index ( )
        {
            string qaJason = QAJason();
            string apJason = ApplicationQAJason();

            List<QA> qaLIst = JsonConvert.DeserializeObject<List<QA>>(qaJason);
            List<vw_GetAppliantQA> apLIst = JsonConvert.DeserializeObject<List<vw_GetAppliantQA>>(apJason);

            GetPassedApplicants(qaLIst, apLIst);

            ViewData["Applicants"] = Applicants;

            return View();
        }

        private void GetPassedApplicants ( List<QA> qaLIst, List<vw_GetAppliantQA> apLIst )
        {
            List<vw_GetAppliantQA> apPassedList = new List<vw_GetAppliantQA>();

            apPassedList = apLIst.Where(a => qaLIst.Any(x => x.Id == a.QAIdFK & x.Answer.Trim() == a.Answer.Trim())).ToList();


            var result = apPassedList.GroupBy(a => a.Id, b => b.Name)
                .Where(x => x.Count() == 10).ToList();


            Applicants = apPassedList.GroupBy(g => new { g.Id })
                         .Where(x => x.Count() == 10)
                         .Select(g => g.First().Name)
                         .ToList();

        }


        #region get mocked jason 


        private string QAJason ( )
        {
            GetEntityList get = new GetEntityList();
            List<QA> aList = get.GetQAList();

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(aList);
            return json;
        }

        private string ApplicationQAJason ( )
        {

            GetEntityList get = new GetEntityList();
            List<vw_GetAppliantQA> aList = get.GetApplicantList();

            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(aList);
            return json;
        }



        #endregion
    }

    internal class DataTable
    {
    }
}
