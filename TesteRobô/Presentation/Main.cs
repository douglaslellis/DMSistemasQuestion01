using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using RobotTest.Business;
using RobotTest.Model;

namespace RobotTest
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        // Ocorre quando o controle WebBrowser termina de carregar um documento.
        private void TestBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            RobotTest.Business.RobotTest _robotTestBusiness = new Business.RobotTest();

            // Url do BING            
            if (TestBrowser.Document.Url.Equals(UntilStrings._BingURL))
            {
                _robotTestBusiness.SearchForABSCard(TestBrowser);
            }
            // Url de pesquisa do BING e Resultado
            else if (TestBrowser.Document.Url.ToString().Contains(UntilStrings._BingURLSearch))
            {
                List<SearchResult> searchResults = _robotTestBusiness.SearchResultForABSCard(TestBrowser);
                WriteResult(searchResults);
            }
            // Fluxo alternativo não previsto
            else
                WriteResultExcepcion();         
        }

        public void WriteResult(List<SearchResult> searchResults)
        {
            var sw = new StreamWriter(Application.StartupPath + UntilStrings._fileResultName);
            foreach (SearchResult searchResult in searchResults)
            {
                sw.WriteLine(searchResult.Title);
                sw.WriteLine(searchResult.Url);
                sw.WriteLine(searchResult.Description);
                sw.WriteLine(UntilStrings._lineBreak);
            }
            sw.Dispose();

            Process.Start("notepad.exe", Application.StartupPath + UntilStrings._fileResultName);
            Close();
        }

        public void WriteResultExcepcion()
        {
            var sw = new StreamWriter(Application.StartupPath + UntilStrings._fileResultName);
            sw.WriteLine(UntilStrings._GenericException);
            sw.Dispose();
            Process.Start("notepad.exe", Application.StartupPath + UntilStrings._fileResultName);
            Close();
        }
    }
}
