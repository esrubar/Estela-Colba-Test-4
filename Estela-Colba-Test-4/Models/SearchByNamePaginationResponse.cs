namespace Estela_Colba_Test_4.Models;


public class SearchByNamePaginationResponse
{

    
    public int CurrentPage { get; set; }
    
    public int TotalPages { get; set; }

    public int ElementsInPage { get; set; }

    public List<Thumbnail> Data { get; set; }
    
    public SearchByNamePaginationResponse(int currentPage, int totalPages, int elementsInPage, List<Thumbnail> data)
    {
        CurrentPage = currentPage;
        TotalPages = totalPages;
        ElementsInPage = elementsInPage;
        Data = data;
    }

}