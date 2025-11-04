namespace ApartadoAulasAPI.Interfaces
{
    public interface ICommonService<T, TI, TU>
    {
        public List<string> Errors { get; }

        Task<IEnumerable<T>> Get();

        Task<T> GetById(int id);

        Task<T> Add(TI CreateRoleDto);

        Task<T> Update(TU UpdateRoleDto);

        void Validate(TI dto);

        bool Validate(TU dto);
    }
}
