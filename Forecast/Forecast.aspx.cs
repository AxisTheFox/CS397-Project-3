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
            forecasts.InnerHtml = "";

            string zipCodes = zipCodeTextBox.Text;

            WeatherService.ndfdXML weatherService = new WeatherService.ndfdXML();

            string latLonXml = weatherService.LatLonListZipCode(zipCodes);
            XmlDocument latLonXmlDocument = new XmlDocument();
            latLonXmlDocument.LoadXml(latLonXml);
            string latLonList = latLonXmlDocument.InnerText;

            string forecastsXml = weatherService.NDFDgenByDayLatLonList(latLonList, DateTime.Today, "5", WeatherService.unitType.e, WeatherService.formatType.Item24hourly);
            XmlDocument forecastsXmlDocument = new XmlDocument();
            forecastsXmlDocument.LoadXml(forecastsXml);


            XmlNodeList locations = forecastsXmlDocument.GetElementsByTagName("parameters");
            foreach (XmlNode location in locations)
            {
                XmlDocument locationXml = new XmlDocument();
                locationXml.LoadXml(location.OuterXml);

                List<double> maxTemperatures = new List<double>();
                List<double> minTemperatures = new List<double>();
                XmlNodeList temperatureNodes = locationXml.GetElementsByTagName("temperature");
                foreach (XmlNode temperatureNode in temperatureNodes)
                {
                    if (temperatureNode.Attributes["type"].Value.Equals("maximum"))
                    {
                        foreach (XmlNode maxTemperature in temperatureNode.ChildNodes)
                        {
                            if (maxTemperature.Name.Equals("value"))
                            {
                                maxTemperatures.Add(double.Parse(maxTemperature.InnerText));
                            }
                        }
                    }
                    if (temperatureNode.Attributes["type"].Value.Equals("minimum"))
                    {
                        foreach (XmlNode minTemperature in temperatureNode.ChildNodes)
                        {
                            if (minTemperature.Name.Equals("value"))
                            {
                                minTemperatures.Add(double.Parse(minTemperature.InnerText));
                            }
                        }
                    }
                }

                List<string> conditionIconUrls = new List<string>();
                XmlNodeList conditionIconUrlNodes = locationXml.GetElementsByTagName("conditions-icon");
                foreach (XmlNode conditionIconUrl in conditionIconUrlNodes)
                {
                    foreach (XmlNode iconUrlNode in conditionIconUrl.ChildNodes)
                    {
                        if (iconUrlNode.Name.Equals("icon-link"))
                        {
                            conditionIconUrls.Add(iconUrlNode.InnerText);
                        }
                    }
                }

                List<string> descriptions = new List<string>();
                XmlNodeList weatherDescriptionNodes = locationXml.GetElementsByTagName("weather");
                foreach (XmlNode weatherDescriptions in weatherDescriptionNodes)
                {
                    foreach (XmlNode descriptionNode in weatherDescriptions.ChildNodes)
                    {
                        if (descriptionNode.Name.Equals("weather-conditions"))
                        {
                            descriptions.Add(descriptionNode.Attributes["weather-summary"].InnerText);
                        }
                    }
                }

                forecasts.InnerHtml += "<p><table>";

                forecasts.InnerHtml += "<tr>";
                foreach(double maxTemp in maxTemperatures)
                {
                    forecasts.InnerHtml += "<td>";
                    forecasts.InnerHtml += "High: " +  maxTemp;
                    forecasts.InnerHtml += "</td>";
                }
                forecasts.InnerHtml += "</tr>";

                forecasts.InnerHtml += "<tr>";
                foreach (string iconUrl in conditionIconUrls)
                {
                    forecasts.InnerHtml += "<td>";
                    forecasts.InnerHtml += "<img src=" + iconUrl + ">";
                    forecasts.InnerHtml += "</td>";
                }
                forecasts.InnerHtml += "</tr>";

                forecasts.InnerHtml += "<tr>";
                foreach (double minTemp in minTemperatures)
                {
                    forecasts.InnerHtml += "<td>";
                    forecasts.InnerHtml += "Low: " + minTemp;
                    forecasts.InnerHtml += "</td>";
                }
                forecasts.InnerHtml += "</tr>";

                forecasts.InnerHtml += "<tr>";
                foreach (string weatherDescription in descriptions)
                {
                    forecasts.InnerHtml += "<td>";
                    forecasts.InnerHtml += weatherDescription;
                    forecasts.InnerHtml += "</td>";
                }
                forecasts.InnerHtml += "</tr>";

                forecasts.InnerHtml += "</table></p>";
            }
        }
    }
}