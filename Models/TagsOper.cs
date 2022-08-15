using System.Text.RegularExpressions;

namespace homePage2.Models;

public static class TagsOper
{
    /// <summary>
    /// Removes html tags from list of articles.
    /// </summary>
    /// <param name="newsList"> List of articles for tags removing.</param>
    /// <returns>List of articles without html tags.</returns>
    public static List<Article> RemoveTags(List<Article> newsList)
    /*removes html tags from articles*/
    {
        for (var i = 0; i < newsList.Count; i++)
        {
            newsList[i].Title = Regex.Replace(newsList[i].Title, "<.*?>", string.Empty);
            newsList[i].Content = Regex.Replace(newsList[i].Content, "<.*?>", string.Empty);
        }

        return newsList;
    }

    /// <summary>
    /// Removes html tags from one article.
    /// </summary>
    /// <param name="article">Article for tags removing.</param>
    /// <returns>Article without html tags.</returns>
    public static Article RemoveTags(Article article)
    /*removes html tags from one article*/
    {

        article.Title = Regex.Replace(article.Title, "<.*?>", string.Empty);
        article.Content = Regex.Replace(article.Content, "<.*?>", string.Empty);

        return article;
    }

    public static List<Article> AddTags(List<Article> newsList)
    /*adds formating to articles*/
    {
        for (var i = 0; i < newsList.Count; i++)
        {
            newsList[i].Content = newsList[i].Content.Replace(Environment.NewLine, "<br>");

            string pattern = @"(\w|['-_!#^~]|\.)+@(\w|\-)(\w|\.|\-)*(\w|\-)";
            MatchCollection matches = Regex.Matches(newsList[i].Content, pattern);
            foreach (Match match in matches)
            {
                newsList[i].Content = newsList[i].Content.Replace(match.Value, "<a href=\"mailto: " + match.Value + "\">" + match.Value + @"</a>");
            }

            pattern = @"https?://(\w|\-)(\w|\.|\-)+(\w|\.|\/|\u003f|[-=$–_+!*‘(),])*";
            matches = Regex.Matches(newsList[i].Content, pattern);
            foreach (Match match in matches)
            {
                newsList[i].Content = newsList[i].Content.Replace(match.Value, "<a href=\"" + match.Value + "\">" + match.Value + @"</a>");
            }
        }

        return newsList;
    }

    public static Article AddTags(Article article)
    /*adds formating to articles*/
    {
        article.Content = article.Content.Replace(Environment.NewLine, "<br>");

        string pattern = @"(\w|['-_!#^~]|\.)+@(\w|\-)(\w|\.|\-)*(\w|\-)";
        MatchCollection matches = Regex.Matches(article.Content, pattern);
        foreach (Match match in matches)
        {
            article.Content = article.Content.Replace(match.Value, "<a href=\"mailto: " + match.Value + "\">" + match.Value + @"</a>");
        }

        pattern = @"https?://(\w|\-)(\w|\.|\-)+(\w|\.|\/|\u003f|[-=$–_+!*‘(),])*";
        matches = Regex.Matches(article.Content, pattern);
        foreach (Match match in matches)
        {
            article.Content = article.Content.Replace(match.Value, "<a href=\"" + match.Value + "\">" + match.Value + @"</a>");
        }

        return article;
    }
}