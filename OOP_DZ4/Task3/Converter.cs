using System;

namespace Task3
{
    public class Converter
    {
        private decimal _usdRate;
        private decimal _eurRate;

        public decimal UsdRate
        {
            get { return _usdRate; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Курс долара має бути більшим за нуль.");
                }
                _usdRate = value;
            }
        }

        public decimal EurRate
        {
            get { return _eurRate; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Курс євро має бути більшим за нуль.");
                }
                _eurRate = value;
            }
        }

        public Converter(decimal usd, decimal eur)
        {
            UsdRate = usd;
            EurRate = eur;
        }

        public decimal ConvertUahToUsd(decimal uahAmount)
        {
            return uahAmount / _usdRate;
        }

        public decimal ConvertUahToEur(decimal uahAmount)
        {
            return uahAmount / _eurRate;
        }

        public decimal ConvertUsdToUah(decimal usdAmount)
        {
            return usdAmount * _usdRate;
        }

        public decimal ConvertEurToUah(decimal eurAmount)
        {
            return eurAmount * _eurRate;
        }
    }
}