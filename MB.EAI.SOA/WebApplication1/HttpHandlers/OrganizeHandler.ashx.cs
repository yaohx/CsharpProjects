using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MB.EAI.SOA.OERP.IFace;
using MB.EAI.SOA.OERP.Impl;

namespace MB.SOA.Web.HttpHandlers
{
    /// <summary>
    /// Summary description for OrganizeHandler
    /// </summary>
    public class OrganizeHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            IOrganizeService orgProxy = new OrganizSerivce();
            context.Response.Write(orgProxy.GetShopsInfo());
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}