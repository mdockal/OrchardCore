using System.Text.Json;
using OrchardCore.ContentManagement;
using OrchardCore.Title.Models;

namespace OrchardCore.Tests.ContentManagement;
public class ContentElementTests
{
    [Fact]
    public void Get_WhenCastingBaseTypeThenConcreteType_ReturnNewInstance()
    {
        var contentItem = new ContentItem();
        var titlePart = new TitlePart
        {
            Title = "test"
        };

        contentItem.Weld(titlePart);

        var json = JConvert.SerializeObject(contentItem);

        var contentItem2 = JConvert.DeserializeObject<ContentItem>(json);

        // act
        // The order arrangement of the next two calls are important.
        // First cast to ContentPart.
        var contentPart = contentItem2.Get<ContentPart>(nameof(TitlePart));

        // Second, cast to TitlePart.
        var actualPart = contentItem2.Get<TitlePart>(nameof(TitlePart));

        // assert
        Assert.NotNull(contentPart);
        Assert.NotNull(actualPart);

        // actualPart should deserialized again, so it must not be same as contentPart.
        Assert.NotSame(contentPart, actualPart);
    }

    [Fact]
    public void Get_WhenCastingConcreteTypeThenBaseType_ReturnNewInstance()
    {
        var contentItem = new ContentItem();
        var titlePart = new TitlePart
        {
            Title = "test"
        };

        contentItem.Weld(titlePart);

        var json = JConvert.SerializeObject(contentItem);

        var contentItem2 = JConvert.DeserializeObject<ContentItem>(json);

        // act
        // The order arrangement of the next two calls are important.
        // First cast to TitlePart.
        var actualPart = contentItem2.Get<TitlePart>(nameof(TitlePart));

        // Second, cast to ContentPart.
        var contentPart = contentItem2.Get<ContentPart>(nameof(TitlePart));

        // assert
        Assert.NotNull(contentPart);
        Assert.NotNull(actualPart);

        // contentPart should be returned from cache, so it must be same as actualPart.
        Assert.Same(contentPart, actualPart);
    }
}
