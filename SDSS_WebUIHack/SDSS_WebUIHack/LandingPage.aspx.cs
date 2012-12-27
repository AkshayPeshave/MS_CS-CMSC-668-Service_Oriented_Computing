using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Diagnostics;
using System.Xml;
using System.Xml.XPath;
using System.Net;
using System.Xml.Serialization;
using System.Web.UI.HtmlControls;

namespace SDSS_WebUIHack
{
    public partial class LandingPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string sdssServiceBasePath = "http://localhost:8080/SDSS/rest?" +
                "skip_auth=true&method=METHOD4&format=xml&start=0&rows=100";
            sdssServiceBasePath += "&dblRightAscensionMin=" + tbMinRA.Text +
                "&dblRightAscensionMax=" + tbMaxRA.Text +
                "&dblDeclensionMin=" + tbMinDec.Text +
                "&dblDeclensionMax=" + tbMaxDec.Text;




            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(sdssServiceBasePath);
            WebResponse serviceResponse = myReq.GetResponse();

            hlSDSSService.NavigateUrl = sdssServiceBasePath;


            XPathNavigator nav;
            XPathDocument docNav;
            XPathNodeIterator NodeIter;
            string strExpression;

            // Open the XML.
            docNav = new XPathDocument(serviceResponse.GetResponseStream());

            // Create a navigator to query with XPath.
            nav = docNav.CreateNavigator();

            //expression to read GSC objects
            strExpression = "/myResponse/body/size";

            // Select the node and place the results in an iterator.
            NodeIter = nav.Select(strExpression);
            NodeIter.MoveNext();
            lblNumFound.Text = NodeIter.Current.Value;

            strExpression = "/myResponse/body/*";

            TableRow row = new TableRow();
            TableCell cell = new TableCell();

            NodeIter = nav.Select(strExpression);
            //Iterate through the results showing the element value.
            while (NodeIter.MoveNext())
            {
                if (NodeIter.Current.LocalName == "size")
                {
                    lblNumFound.Text = NodeIter.Current.Value;
                }
                else
                {
                    row = new TableRow();
                    HyperLink imagelink = new HyperLink();
                    string imageName = "";
                    XPathNodeIterator innerIterator = NodeIter.Current.SelectChildren(XPathNodeType.Element);
                    while (innerIterator.MoveNext())
                    {
                        
                        if (innerIterator.Current.LocalName == "constellation")
                        {
                            cell = new TableCell();
                            cell.Text = innerIterator.Current.Value;
                            row.Cells.Add(cell);
                        }
                        else if (innerIterator.Current.LocalName == "imageURL")
                        {
                            imagelink.NavigateUrl = "~/ShowImage.aspx?imageURL=" + innerIterator.Current.Value;
                        }
                        else
                        {
                            imagelink.Text = innerIterator.Current.Value;
                            imageName = "&imageName=" + innerIterator.Current.Value;
                        }
                    }
                    imagelink.NavigateUrl += "&imageName=" + imageName;
                    cell = new TableCell();
                    cell.Controls.Add(imagelink);
                    row.Cells.Add(cell);
                    tblSDSSImages.Rows.Add(row);
                }

            }
        }
    }
}