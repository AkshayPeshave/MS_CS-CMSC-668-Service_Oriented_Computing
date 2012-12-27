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


    public partial class WebForm1 : System.Web.UI.Page
    {
        //protected HtmlTable tblGSCObjectsInCell;
        protected void Page_Load(object sender, EventArgs e)
        {
            string imageUrl = Request.Params["imageUrl"].ToString();
            ImageMap1.ImageUrl = imageUrl;


            //string imageMetafileUrl = Request.Params["imageUrl"].ToString();
            string imageMetafileUrl = imageUrl.Substring(0, imageUrl.Length - 9) + ".meta.txt";
            
            //string[] lines = System.IO.File.ReadAllLines(@"D:\\Workspace\\CMSC 668\\Catalogs\\fits1.meta.txt");
           
            //string[] lines = System.IO.File.ReadAllLines(imageMetafileUrl);
            
            
            string metaContents;
            using (WebClient client = new WebClient())
            {
                 metaContents = client.DownloadString(imageMetafileUrl);
            }
            string[]  lines = metaContents.Split('\n');
            
            
            string ctype1 = lines[8];
            double RA0, DEC0, X0, Y0, drax, dray, ddecx, ddecy;
            //Label1.Text = lines[11].Split(' ')[1];

            X0 = double.Parse(lines[10].Split(' ')[1], System.Globalization.NumberStyles.Float);

            Y0 = double.Parse(lines[11].Split(' ')[1], System.Globalization.NumberStyles.Float);
            if ((ctype1.Split(' ')[1])[0] == 'R')
            {
                RA0 = double.Parse(lines[12].Split(' ')[1], System.Globalization.NumberStyles.Float);
                DEC0 = double.Parse(lines[13].Split(' ')[1], System.Globalization.NumberStyles.Float);

                drax = double.Parse(lines[14].Split(' ')[1], System.Globalization.NumberStyles.Float);
                dray = double.Parse(lines[15].Split(' ')[1], System.Globalization.NumberStyles.Float);

                ddecx = double.Parse(lines[16].Split(' ')[1], System.Globalization.NumberStyles.Float);
                ddecy = double.Parse(lines[17].Split(' ')[1], System.Globalization.NumberStyles.Float);
            }
            else
            {
                RA0 = double.Parse(lines[13].Split(' ')[1], System.Globalization.NumberStyles.Float);
                DEC0 = double.Parse(lines[12].Split(' ')[1], System.Globalization.NumberStyles.Float);

                drax = double.Parse(lines[16].Split(' ')[1], System.Globalization.NumberStyles.Float);
                dray = double.Parse(lines[17].Split(' ')[1], System.Globalization.NumberStyles.Float);

                ddecx = double.Parse(lines[14].Split(' ')[1], System.Globalization.NumberStyles.Float);
                ddecy = double.Parse(lines[15].Split(' ')[1], System.Globalization.NumberStyles.Float);
            }


            int rightMax = int.Parse(lines[1].Split(' ')[1]);

            int bottomMax = int.Parse(lines[0].Split(' ')[1]);
            int gridCellSize = 1000;
            int x = 0;
            int y = 0;
            int xMax = int.Parse(Math.Truncate(((double)rightMax / gridCellSize)).ToString());
            int yMax = int.Parse(Math.Truncate(((double)bottomMax / gridCellSize)).ToString());
            RectangleHotSpot skySection;
            double[] ra = new double[4];
            double[] dec = new double[4];
            double minRA, maxRA, minDec, maxDec;
            while (y <= yMax)
            {
                x = 0;
                while (x <= xMax)
                {
                    skySection = new RectangleHotSpot();
                    skySection.AlternateText = "Section(" + x + "," + y + ")";
                    skySection.HotSpotMode = HotSpotMode.PostBack;
                    skySection.Target = "_self";


                    ra[0] = RA0 + drax * ((x * gridCellSize) - X0) + dray * ((y * gridCellSize) - Y0);
                    ra[1] = RA0 + drax * (((x * gridCellSize) + gridCellSize) - X0) + dray * ((y * gridCellSize) - Y0);
                    ra[2] = RA0 + drax * ((x * gridCellSize) - X0) + dray * (((y * gridCellSize) + gridCellSize) - Y0);
                    ra[3] = RA0 + drax * (((x * gridCellSize) + gridCellSize) - X0) + dray * (((y * gridCellSize) + gridCellSize) - Y0);
                    maxRA = ra.Max();
                    minRA = ra.Min();

                    dec[0] = DEC0 + ddecx * ((x * gridCellSize) - X0) + ddecy * ((y * gridCellSize) - Y0);
                    dec[1] = DEC0 + ddecx * (((x * gridCellSize) + gridCellSize) - X0) + dray * ((y * gridCellSize) - Y0);
                    dec[2] = DEC0 + ddecx * ((x * gridCellSize) - X0) + dray * (((y * gridCellSize) + gridCellSize) - Y0);
                    dec[3] = DEC0 + ddecx * (((x * gridCellSize) + gridCellSize) - X0) + dray * (((y * gridCellSize) + gridCellSize) - Y0);
                    maxDec = dec.Max();
                    minDec = dec.Min();


                    skySection.PostBackValue = minRA.ToString() + " " + maxRA.ToString() + " " +
                        minDec.ToString() + " " + maxDec.ToString();
                    skySection.Left = x * gridCellSize;
                    skySection.Bottom = (y * gridCellSize) + gridCellSize;

                    skySection.Top = y * gridCellSize;
                    skySection.Right = (x * gridCellSize) + gridCellSize;

                    ImageMap1.HotSpots.Add(skySection);

                    x++;
                }

                y++;
            }


            string sdssServiceBasePath = "http://localhost:8080/SDSS/rest?" +
                "skip_auth=true&method=METHOD5&format=xml&start=0&rows=100";
            sdssServiceBasePath += "&imageName=" + Server.UrlEncode(Request.Params["imageName"].ToString().Substring(1));




            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(sdssServiceBasePath);
            WebResponse serviceResponse = myReq.GetResponse();

            hlAllSDSSObjects.NavigateUrl = sdssServiceBasePath;

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
            Label3.Text = "SDSS Objects in Image : " + NodeIter.Current.Value;

            strExpression = "/myResponse/body/*";

            TableRow row = new TableRow();
            TableCell cell = new TableCell();

            NodeIter = nav.Select(strExpression);
            //Iterate through the results showing the element value.
            while (NodeIter.MoveNext())
            {
                if (NodeIter.Current.LocalName == "size")
                {
                    Label3.Text = "SDSS Objects in Image : " + NodeIter.Current.Value;
                }
                else
                {
                    row = new TableRow();

                    //gen.InnerHtml += "<tr>";
                    XPathNodeIterator innerIterator = NodeIter.Current.SelectChildren(XPathNodeType.Element);
                    while (innerIterator.MoveNext())
                    {
                        cell = new TableCell();
                        cell.Text = innerIterator.Current.Value;
                        row.Cells.Add(cell);
                    }
                    tblAllSDSSObjects.Rows.Add(row);
                }
            }
        }

        protected void ImageMap1_Click(object sender, ImageMapEventArgs e)
        {
            string[] cellBoundaries = e.PostBackValue.Split(' ');

            string sdssServiceBasePath = "http://localhost:8080/SDSS/rest?" +
                "skip_auth=true&method=METHOD3&format=xml&start=0&rows=100";
            sdssServiceBasePath += "&dblRightAscensionMin=" + cellBoundaries[0] +
                "&dblRightAscensionMax=" + cellBoundaries[1] +
                "&dblDeclensionMin=" + cellBoundaries[2] +
                "&dblDeclensionMax=" + cellBoundaries[3];
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(sdssServiceBasePath);
            WebResponse serviceResponse = myReq.GetResponse();

            hlGSCObjects.NavigateUrl = sdssServiceBasePath;


            XPathNavigator nav;
            XPathDocument docNav;
            XPathNodeIterator NodeIter;
            string strExpression;

            // Open the XML.
            docNav = new XPathDocument(serviceResponse.GetResponseStream());

            // Create a navigator to query with XPath.
            nav = docNav.CreateNavigator();

            //expression to read GSC objects
            strExpression = "/myResponse/body/response/numFound";

            // Select the node and place the results in an iterator.
            NodeIter = nav.Select(strExpression);
            NodeIter.MoveNext();
            Label1.Text = "GSC Objects Found in Cell : " + NodeIter.Current.Value;

            strExpression = "/myResponse/body/response/docs";

            //HtmlGenericControl gen = new HtmlGenericControl("table");
            //Table t = new Table();
            TableRow row = new TableRow();
            TableCell cell = new TableCell();

            NodeIter = nav.Select(strExpression);
            //Iterate through the results showing the element value.
            while (NodeIter.MoveNext())
            {
                row = new TableRow();

                //gen.InnerHtml += "<tr>";
                XPathNodeIterator innerIterator = NodeIter.Current.SelectChildren(XPathNodeType.Element);
                while (innerIterator.MoveNext())
                {
                    cell = new TableCell();
                    cell.Text = innerIterator.Current.Value;
                    row.Cells.Add(cell);
                }
                tblGSCObjects.Rows.Add(row);

            }
                ////////////////////////////////////////////////////////////////////////


                sdssServiceBasePath = "http://localhost:8080/SDSS/rest?" +
                "skip_auth=true&method=METHOD2&format=xml&start=0&rows=100";
            sdssServiceBasePath += "&dblRightAscensionMin=" + cellBoundaries[0] +
                "&dblRightAscensionMax=" + cellBoundaries[1] +
                "&dblDeclensionMin=" + cellBoundaries[2] +
                "&dblDeclensionMax=" + cellBoundaries[3];


            

            myReq = (HttpWebRequest)WebRequest.Create(sdssServiceBasePath);
            serviceResponse = myReq.GetResponse();

            hlSDSSObject.NavigateUrl = sdssServiceBasePath;


            

            // Open the XML.
            docNav = new XPathDocument(serviceResponse.GetResponseStream());

            // Create a navigator to query with XPath.
            nav = docNav.CreateNavigator();

            //expression to read GSC objects
            strExpression = "/myResponse/body/size";

            // Select the node and place the results in an iterator.
            NodeIter = nav.Select(strExpression);
            NodeIter.MoveNext();
            Label2.Text = "SDSS Objects Found in Cell : " + NodeIter.Current.Value;

            strExpression = "/myResponse/body/*";

            

            NodeIter = nav.Select(strExpression);
            //Iterate through the results showing the element value.
            while (NodeIter.MoveNext())
            {
                if (NodeIter.Current.LocalName == "size")
                {
                    Label2.Text = "SDSS Objects Found in Cell : " + NodeIter.Current.Value;
                }
                else
                {
                    row = new TableRow();

                    //gen.InnerHtml += "<tr>";
                    XPathNodeIterator innerIterator = NodeIter.Current.SelectChildren(XPathNodeType.Element);
                    while (innerIterator.MoveNext())
                    {
                        cell = new TableCell();
                        cell.Text = innerIterator.Current.Value;
                        row.Cells.Add(cell);
                    }
                    tblSDSSObjects.Rows.Add(row);
                }
            }
           
        }
    }

    

    public class GSCObject
    {
        public int longGscID;
        public double dblRightAscension;
        public double dblDeclension;
        public float fltPosError;
        public float fltMagnitude;
        public float fltMagnitudeError;
        public int intBand;
        public int intClass;
        public string strPlateNum;
        public int intMultipleObjects;
        public int dblRAMin;
        public double dblRAMax;
        public double dblDecMin;
        public double dblDecMax;

    }

    public class SDSSObject
    {
        public string basePath;
        public string imageName;
        
        public int ojbId;
        
        public double rightAscension;
        
        public double declension;
        
        public double majorAxis;
        
        public double minorAxis;
        
        public double eccentricity;
        
        public double theta;
        
        public int solidAngle;
        
        public int count;
        
        public int numPixels;
        
        public double magnitude;

        public string constellation;
        
    }
}