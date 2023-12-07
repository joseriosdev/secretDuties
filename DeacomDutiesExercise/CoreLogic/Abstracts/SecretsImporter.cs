using DeacomDutiesExercise.Models.Interfaces;
using DeacomDutiesExercise.Utils;

namespace DeacomDutiesExercise.CoreLogic.Abstracts
{
    public abstract class SecretsImporter<Tdto, TEntity>
        where Tdto : ISecretDTO, new()
        where TEntity : new()
    {
        protected readonly LogBook _log;
        public SecretsImporter()
        {
            _log = new LogBook();
        }

        public abstract List<TEntity> MapSecretsToDBEntity(List<Tdto> secrets);
        public abstract void BulkInsertSecrets(List<TEntity> secrets);
    }
}
