using AutoMapper;
using Castro.AluguelDeCarros.Reserva.Domain;
using Castro.AluguelDeCarros.Reserva.Domain.Infrastructure;
using Castro.AluguelDeCarros.Reserva.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Castro.AluguelDeCarros.Reserva.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;

        public CategoriaService(ICategoriaRepository categoriaRepository, IMapper mapper)
        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Categoria>> BuscarTodas()
        {
            return await _categoriaRepository.BuscarTodasCategorias();
        }

        public async Task<Categoria> Obter(Guid id)
        {
            return await _categoriaRepository.BuscarCategoria(id);
        }

        public async Task<Categoria> Salvar(Guid? id, string nome)
        {
            Categoria categoria;

            if (id.HasValue)
            {
                categoria = await _categoriaRepository.BuscarCategoria(id.Value);

                if (categoria != null && categoria.Id != Guid.Empty)
                {
                    categoria.DefinirOuAlterarNome(nome);
                    if (categoria.Valido)
                        await _categoriaRepository.AtualizarCategoria(categoria);
                }
            }
            else
            {
                categoria = _mapper.Map<Categoria>(nome);
                await _categoriaRepository.SalvarCategoria(categoria);
            }

            return categoria;
        }
    }
}
