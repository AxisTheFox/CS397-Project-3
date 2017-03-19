using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Forecast
{
    public partial class Forecast : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void submitButton_Click(object sender, EventArgs e)
        {
            string zipCodes = zipCodeTextBox.Text;

            WeatherService.ndfdXML weatherService = new WeatherService.ndfdXML();

            string latLonXml = weatherService.LatLonListZipCode(zipCodes);
            XmlDocument latLonXmlDocument = new XmlDocument();
            latLonXmlDocument.LoadXml(latLonXml);
            string latLonList = latLonXmlDocument.InnerText;
        }
    }
}