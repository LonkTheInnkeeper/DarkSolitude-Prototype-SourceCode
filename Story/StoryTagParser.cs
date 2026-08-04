using System.Collections.Generic;
using Ink.Runtime;

public static class StoryTagParser
{
    public static StoryTagData Parse(Story story)
    {
        StoryTagData data = new StoryTagData();

        foreach (string tag in story.currentTags)
        {
            string[] split = tag.Split(':');

            if (split.Length != 2)
                continue;

            string key = split[0].Trim().ToLower();
            string value = split[1].Trim();

            switch (key)
            {
                case "header":
                    data.header = value; break;
                case "speaker":
                    data.speaker = value; break;
                case "line":
                    data.line = value; break;
                case "action":
                    data.action = value; break;
                case "background":
                    data.background = value; break;
            }
        }

        return data;
    }
}
