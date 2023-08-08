namespace ArthurCallouts.Services
{
    public enum WeaponStatus
    {
        None,         // Sem arma
        Legal,        // Arma legal
        Illegal       // Arma ilegal
    }

    public enum DrivingStatus
    {
        NoLicense,      // Sem licença
        ValidLicense,   // Licença válida
        Suspended       // Licença suspensa
    }

    public enum IdentityStatus
    {
        Real,     // Identidade real
        Fake      // Identidade falsa
    }

    public enum TransportMode
    {
        OnFoot,        // A pé
        InCar,         // Em um carro
        OnMotorcycle   // Em uma motocicleta
    }

    public enum PossessionStatus
    {
        None,       // Sem posse
        LegalItem,  // Item legal
        IllegalItem // Item ilegal
    }

    public enum WantedStatus
    {
        NotWanted,     // Não é procurado
        Wanted         // É procurado
    }
}

namespace ArthurCallouts.Services
{
    using System;

    public class RandomizationService
    {
        private Random _Random = new Random();

        public WeaponStatus RandomizeWeaponStatus()
        {
            int randVal = _Random.Next(3);
            return (WeaponStatus)randVal;
        }

        public DrivingStatus RandomizeDrivingStatus()
        {
            int randVal = _Random.Next(3);
            return (DrivingStatus)randVal;
        }

        public IdentityStatus RandomizeIdentityStatus()
        {
            int randVal = _Random.Next(2);
            return (IdentityStatus)randVal;
        }

        public TransportMode RandomizeTransportMode()
        {
            int randVal = _Random.Next(3);
            return (TransportMode)randVal;
        }

        public PossessionStatus RandomizePossession()
        {
            int randVal = _Random.Next(3);
            return (PossessionStatus)randVal;
        }

        public WantedStatus RandomizeWantedStatus()
        {
            int randVal = _Random.Next(2);
            return (WantedStatus)randVal;
        }
    }
}
