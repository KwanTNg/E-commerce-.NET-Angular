using System;

namespace Core.Specifications;

//Filtering by more than one brands
public class ProductSpecsParams : PagingParams
{

    private List<string> _brands = [];
    public List<string> Brands
    {
        get => _brands; // types=boards,gloves
        set
        {
            _brands = value.SelectMany(x => x.Split(',',
            StringSplitOptions.RemoveEmptyEntries)).ToList();
        }
    }

    private List<string> _types = [];
    public List<string> Types
    {
        get => _types; // types=boards,gloves
        set
        {
            _types = value.SelectMany(x => x.Split(',',
            StringSplitOptions.RemoveEmptyEntries)).ToList();
        }
    }
    public string? Sort { get; set; }

    //Search functionality
    private string? _search;
    public string Search
    {
        get => _search ?? "";
        set => _search = value.ToLower();
    }

}
