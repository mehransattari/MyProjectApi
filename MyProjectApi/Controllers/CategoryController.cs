using Data.Contracts;
using Entittes;
using Microsoft.AspNetCore.Mvc;
using WebFramework.Api;

namespace MyProjectApi.Controllers;

public class CategoryController : BaseController
{
    private readonly IRepository<Category> categoryRepository;
    public CategoryController(IRepository<Category> categoryRepository)
    {
        this.categoryRepository = categoryRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> Get(CancellationToken cancellationToken)
    {
        var Categorys = await categoryRepository.GetAllAsync(cancellationToken);
        return Ok(Categorys);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Category>> Get(int id, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(cancellationToken, id);
        return category;
    }

    [HttpPost]
    public async Task Create(Category Category, CancellationToken cancellationToken)
    {
        await categoryRepository.AddAsync(Category, cancellationToken);
    }

    [HttpPut]
    public async Task<ActionResult> Update(Category Category, CancellationToken cancellationToken)
    {
        categoryRepository.Attach(Category);
        await categoryRepository.UpdateAsync(Category, cancellationToken);
        return Ok();
    }

    [HttpDelete]
    public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var Category = await categoryRepository.GetByIdAsync(cancellationToken, id);
        await categoryRepository.DeleteAsync(Category, cancellationToken);
        return Ok();
    }
}
