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
            WeatherService.ndfdXML weather = new WeatherService.ndfdXML();
            string forecast = weather.NDFDgenByDay(-45, 88, DateTime.Today, "5", WeatherService.unitType.e, WeatherService.formatType.Item12hourly);
            XmlDocument nwsWeatherForecast = new XmlDocument();
            nwsWeatherForecast.LoadXml(forecast);
            XmlNodeList temperature = nwsWeatherForecast.GetElementsByTagName("temperature");
        }
    }
}