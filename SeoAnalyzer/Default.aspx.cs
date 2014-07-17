using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using HtmlAgilityPack;

namespace SeoAnalyzer
{
    public partial class MainForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void StartBtn_Click(object sender, EventArgs e)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument htmlDoc = new HtmlDocument();
            string input_text = "";
            bool wrong_url = false;
            if (TextOrUrl.Items[1].Selected)
            {
                string Url = Input.Text;
                if (!Url.ToLower().StartsWith("http://") && !Url.ToLower().StartsWith("https://"))
                {
                    Url = "http://" + Url;
                }
                try
                {
                    htmlDoc = web.Load(Url);
                    input_text = HtmlAnalyzer.GetTextFromHtml(htmlDoc);
                }
                catch (WebException ex)
                {
                    InfoString.Text = ex.Message;
                    wrong_url = true;
                }
            }

            if (TextOrUrl.Items[0].Selected)
            {
                input_text = Input.Text;
            }


            Dictionary<string, int> words = new Dictionary<string, int>();
            words = Filter.GetDict(input_text);

            //Filter stop words
            if (AnalysisOpt.Items[0].Selected)
            {
                Filter.RemoveStopWords(words);
            }

            //Word counts
            if (AnalysisOpt.Items[1].Selected)
            {
                WordsGrid.Visible = true;
                WordsGrid.DataSource = words.ToList();
                WordsGrid.DataBind();
            }
            else
            {
                WordsGrid.Visible = false;
            }

            //Words in meta tags
            MetaWordsGrid.Visible = false;
            if ((AnalysisOpt.Items[2].Selected) && (TextOrUrl.Items[1].Selected) && (wrong_url != true))
            {
                Dictionary<string, int> meta_words = new Dictionary<string, int>();
                meta_words = HtmlAnalyzer.GetMetaWords(words, htmlDoc);
                if (meta_words.Count != 0)
                {
                    MetaWordsGrid.Visible = true;
                    MetaWordsGrid.DataSource = meta_words.ToList();
                    MetaWordsGrid.DataBind();
                }
                else
                {
                    MetaWordsGrid.Visible = false;
                    MetaWordsInfo.Text = "No meta keywords";
                }
            }

            //External links count
            if ((AnalysisOpt.Items[3].Selected) && (TextOrUrl.Items[1].Selected) && (wrong_url != true))
            {
                LinksNum.Visible = true;
                LinksNum.Text = "Number of links: " + HtmlAnalyzer.GetCount(htmlDoc).ToString();
            }
            else
            {
                LinksNum.Visible = false;
            }
        }


        public string GridViewSortDirection(string column)
        {
            string sortDirection = "ASC";
            string lastDirection = ViewState["SortDirection"] as string;
            if (lastDirection == "ASC")
                {
                    sortDirection = "DESC";
                }
            ViewState["SortDirection"] = sortDirection;
            return sortDirection;
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Key");
            dt.Columns.Add("Value", typeof(int));

            foreach (GridViewRow row in WordsGrid.Rows)
            {
                DataRow dr;
                dr = dt.NewRow();

                for (int i = 0; i < row.Cells.Count; i++)
                {
                    dr[i] = row.Cells[i].Text;
                }
                dt.Rows.Add(dr);
            }

            if (dt != null)
            {
                dt.DefaultView.Sort = e.SortExpression + " " + GridViewSortDirection(e.SortExpression);
                WordsGrid.DataSource = dt;
                WordsGrid.DataBind();
            }
        }

        protected void GridView2_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Key");
            dt.Columns.Add("Value", typeof(int));

            foreach (GridViewRow row in MetaWordsGrid.Rows)
            {
                DataRow dr;
                dr = dt.NewRow();

                for (int i = 0; i < row.Cells.Count; i++)
                {
                    dr[i] = row.Cells[i].Text;
                }
                dt.Rows.Add(dr);
            }

            if (dt != null)
            {
                dt.DefaultView.Sort = e.SortExpression + " " + GridViewSortDirection(e.SortExpression);
                MetaWordsGrid.DataSource = dt;
                MetaWordsGrid.DataBind();
            }
        }

    }
}