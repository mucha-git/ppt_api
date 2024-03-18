namespace WebApi.Entities.ElementsTypes;

public interface INavigation : IElementsTypes
{
    public int? DestinationViewId { get; set; }
}