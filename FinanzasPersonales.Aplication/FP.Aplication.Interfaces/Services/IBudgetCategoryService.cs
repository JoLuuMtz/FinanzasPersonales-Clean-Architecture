


namespace FinanzasPersonales.Aplication;

public interface IBudgetCategoryService

{
    public Task<IEnumerable<BudgetCategoryDTO>> GetAll();
    public Task<ServiceResult<BudgetCategoryDTO>> GetById(int id);
    public Task<ServiceResult<BudgetCategoryDTO>> Create(CreateBudgetCategoryDTO createBudgetCategoryDTO);
    public Task<ServiceResult<BudgetCategoryDTO>> Update(int id, UpdateBudgetCategoryDTO updateBudgetCategoryDTO);
    public Task<ServiceResult<BudgetCategoryDTO>> Delete(int id);

    // retorna tu si hay suficiente dinero en ingresos para cubrir los los prespuestos
    public Task<bool> IncomesEnoght();

    //public Task<bool> BudgetByName(string name);

    //Task<int> VaidatorBudgetId(int idbudget);







}
