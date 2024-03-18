namespace WebApi.Entities.ElementsTypes;

public interface IGraphicAndText : IElementsTypes
{
    public string Text { get; set; }

    public string ImgSrc { get; set; }
}