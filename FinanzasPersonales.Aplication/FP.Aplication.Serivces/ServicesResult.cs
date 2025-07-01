namespace FinanciasPersonalesApiRes
{
   
    public class ServiceResult<T> // clase generica
    {
        // propiedades de la clase
        public bool Success { get; set; }// propiedad de exito
        public T Data { get; set; } // la data que retorna 
        public List<string> Errors { get; set; } = new(); // lista de errores


        // retorna Un objeto con Success true y la data
        public static ServiceResult<T> Ok(T data)
        {
            return new ServiceResult<T> { Success = true, Data = data };
        }

        // retorna un objeto de tipo ServiceResult y recibe un array de errores
        public static ServiceResult<T> Fail(params string[] errors)
        {
            return new ServiceResult<T> { Success = false, Errors = errors.ToList() };
        }
    }
}
