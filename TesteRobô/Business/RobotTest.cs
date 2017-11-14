using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RobotTest.Model;

namespace RobotTest.Business
{
    public class RobotTest
    {
        // Procurar Pelo titulo de ABScard
        public void SearchForABSCard(System.Windows.Forms.WebBrowser broser)
        {
            var searchBox = broser.Document.GetElementById("sb_form_q");
            searchBox.InnerText = UntilStrings._AbscardTitle;

            var searchButton = broser.Document.GetElementById("sb_form_go");
            searchButton.InvokeMember("click");
        }

        public List<SearchResult> SearchResultForABSCard(System.Windows.Forms.WebBrowser broser)
        {
            List<SearchResult> searchResults = new List<SearchResult>();

            var resultsElements = broser.Document.GetElementById("b_results").Children;
            foreach (System.Windows.Forms.HtmlElement result in resultsElements)
            {
                if (result.OuterHtml.Contains("b_algo"))                
                    searchResults.Add(new SearchResult() {
                        Title = result.Children[0].InnerText.Trim(),
                        Url = result.Children[1].InnerText.Trim().Split('\n')[0],
                        Description = result.Children[1].InnerText.Trim().Split('\n')[2]
                    });                
            }

            return searchResults;
        }
    }
}
