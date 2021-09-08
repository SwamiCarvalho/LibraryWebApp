using System.Threading.Tasks;
using System;
using LibraryWebApp.Domain.Repositories;
using LibraryWebApp.Domain.Models;
using LibraryWebApp.Domain.Services.Communication;
using System.Collections.Generic;
using LibraryWebApp.Resources;
using AutoMapper;
using System.Linq;

namespace LibraryWebApp.Services
{
    public class ReaderService : IReaderService
    {

        public IReaderRepository _readerRepository;
        public readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public ReaderService(IReaderRepository readerRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _readerRepository = readerRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ReaderResponse> GetAllReadersAsync()
        {
            var readers = await _readerRepository.ListAsync();

            if (!readers.Any())
                return new ReaderResponse("The website has no Readers :'(  \r\n  Don't worry soon they will start looking for knowledge ;D");

            var readersResource = _mapper.Map<IEnumerable<Reader>, IEnumerable<ReaderResource>>(readers);

            return new ReaderResponse(readersResource);
        }

        public async Task<ReaderResponse> GetReaderByIdAsync(long id)
        {
            var reader = await _readerRepository.GetReaderByIdAsync(id);

            if (reader == null)
                return new ReaderResponse("Reader not found.");

            var readersResource = _mapper.Map<Reader, ReaderResource>(reader);

            return new ReaderResponse(readersResource);
        }

        public async Task<ReaderResponse> GetReaderByIdentityIdAsync(string id)
        {
            var reader = await _readerRepository.GetReaderByIdentityIdAsync(id);

            if (reader == null)
                return new ReaderResponse("Reader not found.");

            var readersResource = _mapper.Map<Reader, ReaderResource>(reader);

            return new ReaderResponse(readersResource);
        }

        public async Task<ReaderResponse> SaveReaderAsync(CreateUserResource readerResource)
        {
            try
            {
                var reader = _mapper.Map<CreateUserResource, Reader>(readerResource);
                _readerRepository.AddReader(reader);
                await _unitOfWork.CompleteAsync();

                var newReaderResource = _mapper.Map<Reader, ReaderResource>(reader);

                return new ReaderResponse(newReaderResource);
            }
            catch (Exception ex)
            {
                return new ReaderResponse($"An error occurred when saving the reader: {ex.Message}");
            }
        }

        public async Task<ReaderResponse> UpdateReaderAsync(long id, UpdateReaderResource readerResource)
        {
            var existingReader = await _readerRepository.GetReaderByIdAsync(id);

            if (existingReader == null)
                return new ReaderResponse("Reader not found.");


            existingReader.Address = readerResource.Address;
            existingReader.FullName = readerResource.FullName;
            existingReader.Email = readerResource.Email;

            try
            {
                _readerRepository.UpdateReader(existingReader);
                await _unitOfWork.CompleteAsync();

                var updatedReader = _mapper.Map<Reader, ReaderResource>(existingReader);

                return new ReaderResponse(updatedReader);
            }
            catch (Exception ex)
            {
                return new ReaderResponse($"An error occurred when updating the reader: {ex.Message}");
            }
        }

        public async Task<ReaderResponse> DeleteReaderAsync(long id)
        {
            var existingReader = await _readerRepository.GetReaderByIdAsync(id);

            if (existingReader == null)
                return new ReaderResponse("Reader not found.");

            try
            {
                _readerRepository.DeleteReader(existingReader);
                await _unitOfWork.CompleteAsync();

                var deletedReader = _mapper.Map<Reader, ReaderResource>(existingReader);

                return new ReaderResponse(deletedReader);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ReaderResponse($"An error occurred when deleting the reader: {ex.Message}");
            }
        }
    }
}