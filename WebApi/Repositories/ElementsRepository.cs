using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.Entities.ElementsTypes;
using WebApi.Helpers;

namespace WebApi.Repositories;

public interface IElementsRepository
{
    Task<IEnumerable<Elements>> Get(int yearId);

    Task<Elements> GetById(int id);
    Task<Elements> Create(Elements model);
    Task<Elements> Update(Elements model);
    Task<IEnumerable<Elements>> UpdateAll();
    Task Delete(Elements model);
}

public class ElementsRepository : IElementsRepository
{
    private readonly DataContext _context;

    public ElementsRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Elements> Create(Elements model)
    {
        try
        {
            foreach (var element in await _context.Elements.Where(v => v.YearId == model.YearId && v.ViewId == model.ViewId && v.Order >= model.Order).ToListAsync())
            {
                element.Order = element.Order + 1;
            }
            await _context.Elements.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }
        catch (Exception e)
        {
            throw new Exception(e.InnerException.Message);
        }
    }

    public async Task<IEnumerable<Elements>> Get(int yearId)
    {
        try
        {
            var ret = await _context.Elements
                .Where(y => y.YearId == yearId)
                .OrderBy(o => o.Order).ToListAsync();
            return ret;
        }
        catch (Exception e)
        {
            throw new Exception(e.InnerException.Message);
        }
    }

    public async Task<Elements> Update(Elements model)
    {
        try
        {
            var oldOrder = _context.Elements.AsNoTracking().First(v => v.Id == model.Id).Order;
            if (model.Order < oldOrder)
            {
                foreach (var element in await _context.Elements.Where(v => v.YearId == model.YearId && v.ViewId == model.ViewId && v.Order >= model.Order && v.Order < oldOrder).ToListAsync())
                {
                    element.Order = element.Order + 1;
                }
            }
            else if (model.Order > oldOrder)
            {
                foreach (var element in await _context.Elements.Where(v => v.YearId == model.YearId && v.ViewId == model.ViewId && v.Order <= model.Order && v.Order > oldOrder).ToListAsync())
                {
                    element.Order = element.Order - 1;
                }
            }

            _context.Elements.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }
        catch (Exception e)
        {
            throw new Exception(e.InnerException.Message);
        }
    }

    public async Task Delete(Elements model)
    {
        try
        {
            _context.Elements.Remove(model);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.InnerException.Message);
        }

    }

    public async Task<Elements> GetById(int id)
    {
        return await _context.Elements.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<Elements>> UpdateAll()
    {
        try
        {
            var elements = await _context.Elements.ToListAsync();
            /*foreach (var e in elements)
            {
                ElementValues ev = new ElementValues()
                {
                    Color = e.Color,
                    Margin = e.Margin,
                    Height = e.Height,
                    Text = e.Text,
                    ImgSrc = e.ImgSrc,
                    Autoplay = e.Autoplay,
                    Playlist = e.Playlist,
                    MapHeight = e.MapHeight,
                    DestinationViewId = e.DestinationViewId
                };
                var jsonOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
                e.Values = JsonSerializer.Serialize(ev, jsonOptions);
            }*/
            _context.Elements.UpdateRange(elements);
            await _context.SaveChangesAsync();
            return elements;
        }
        catch (Exception e)
        {
            throw new Exception(e.InnerException.Message);
        }
    }
}