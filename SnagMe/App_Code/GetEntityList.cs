using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SnagMe.Models;

namespace SnagMe.Entities
{
    public class GetEntityList
    {
        private Models.SnagMeEntities db;

       internal  GetEntityList ( ) {

            db = new SnagMeEntities();
        }

          internal List<QA> GetQAList ( )
        {
            List<QA> list = new List<QA>();

            list = db.QAs.ToList();

            return list;

        }


        internal List<vw_GetAppliantQA> GetApplicantList ( )
        {
            List<vw_GetAppliantQA> list = new List<vw_GetAppliantQA>();

            list = db.vw_GetAppliantQA.ToList();


            return list;

        }
    }
}