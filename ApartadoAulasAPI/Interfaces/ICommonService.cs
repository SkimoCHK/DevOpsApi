namespace ApartadoAulasAPI.Interfaces
{
    public interface ICommonService<T, TI, TU>
    {
        public List<string> Errors { get; }

        Task<IEnumerable<T>> Get();

        Task<T> GetById(int id);

        Task<T> Add(TI CreateEntityDto);

        Task<T> Update(TU UpdateEntityDto);

        void Validate(TI dto);

        void Validate(TU dto);
    }
}
