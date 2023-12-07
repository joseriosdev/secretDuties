using DeacomDutiesExercise.Models.Interfaces;

namespace DeacomDutiesExercise.CoreLogic.Abstracts
{
    public abstract class SecretsImporter<Tdto, TEntity>
        where Tdto : ISecretDTO, new()
        where TEntity : new()
    {
        public abstract List<TEntity> MapSecretsToDBEntity(List<Tdto> secrets);
        public abstract void BulkInsertSecrets(List<TEntity> secrets);
    }
}
