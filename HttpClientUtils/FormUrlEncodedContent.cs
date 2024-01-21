using System.Text;

namespace HttpClientUtils;

internal class FormUrlEncodedContent
{
    private readonly List<KeyValuePair<string, string>> formData;

    public FormUrlEncodedContent()
    {
        formData = [];
    }

    public void AddParameter(string key, string value) => formData.Add(new(key, value));

    public HttpContent ToFormUrlEncodedContent()
    {
        StringBuilder builder = new();

        for (int i = 0; i < formData.Count; i++)
        {
            if (i > 0)
                builder.Append('&');

            builder.Append(Uri.EscapeDataString(formData[i].Key));
            builder.Append('=');
            builder.Append(Uri.EscapeDataString(formData[i].Value));
        }

        string content = builder.ToString();
        return new StringContent(content, Encoding.UTF8, "application/x-www-form-urlencoded");
    }

    public override string ToString()
    {
        StringBuilder builder = new();

        for (int i = 0; i < formData.Count; i++)
        {
            if (i > 0)
                builder.AppendLine();

            builder.Append(formData[i].Key);
            builder.Append(": ");
            builder.Append(formData[i].Value);
        }

        return builder.ToString();
    }
}