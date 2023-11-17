using BugTrackerCore.Interfaces;
using BugTrackerCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerCore.Services
{
    public class ConnectionProcessingService : IConnectionProcessingService
    {
        private readonly ILocalRepository _localRepo;
        private readonly HubConnectionManager _hubConnectionManager;
        private readonly IDbRepository _repository;

        public ConnectionProcessingService(ILocalRepository localRepo, HubConnectionManager hubConnectionManager, IDbRepository repository)
        {
            _localRepo = localRepo;
            _hubConnectionManager = hubConnectionManager;
            _repository = repository;
        }

        public async void ProcessError(ErrorPostModel error, bool appNameExists, bool userIdIsEmpty)
        {
            //if user id doesn't exist and appname doesn't exist add errot to local repo
            if (!userIdIsEmpty && !appNameExists)
            {
                AddErrorToRepositoryAndSignal(error);
                return;
            }

            //checks if application name exists in appNameList
            if (!appNameExists)
            {
                AddErrorToRepositoryAndSignal(error);
            }

            if (!userIdIsEmpty)
            {
                Application existingApplication = _repository.GetApplicationByName(_localRepo.UserId, error.ApplicationName);
                error.ErrorModel.ApplicationId = existingApplication.ApplicationId;
                await _hubConnectionManager.SendError(error);
                _repository.AddError(error.ErrorModel);
                await _hubConnectionManager.SendAppSignal();
            }
        }

        private async void AddErrorToRepositoryAndSignal(ErrorPostModel error)
        {
            _localRepo.AddErrorPostModel(error);
            _localRepo.AddAppName(error.ApplicationName);
            await _hubConnectionManager.SendAppSignal();
        }
    }
}
