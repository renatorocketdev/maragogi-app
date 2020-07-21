using AppTesteBinding.Models;
using AppTesteBinding.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Essentials;

namespace AppTesteBinding.ViewModels
{
    public class EstabelecimentoFotos : INotifyPropertyChanged
    {
        #region Property

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion

        public ObservableCollection<FotosEstabelecimentos> FotosEmpresa { get; }

        public EstabelecimentoFotos(string filtroEmpresa, string FilCat)
        {
            FotosEmpresa = new ObservableCollection<FotosEstabelecimentos>();

            string caminhoEmpresa = string.Empty;

            if (FilCat == "Adega" || FilCat == "Cafe" || FilCat == "Churrascaria" || FilCat == "Gourmet" || FilCat == "Hamburgueria" || FilCat == "Lanchonete" || FilCat == "Oriental" || FilCat == "Pizzaria" || FilCat == "Sorveteria" || FilCat == "Restaurante")
            {
                caminhoEmpresa = "https://appmaragogi.com.br/Fotos/Empresas/Comer/";
            }
            else if(FilCat == "Salão" || FilCat == "Massagem" || FilCat == "Manicure" || FilCat == "Estética" || FilCat == "Cabeleireiro")
            {
                caminhoEmpresa = "https://appmaragogi.com.br/Fotos/Empresas/Beleza/";
            }
            else if (FilCat == "Artigos Religiosos" || FilCat == "Armazém Construção" || FilCat == "Artesanato" || FilCat == "Casa do Bolo" || FilCat == "Farmácia" || FilCat == "Produtos Naturais" || FilCat == "Aves, Peixes e Carnes" || FilCat == "Padaria" || FilCat == "Moda" || FilCat == "Mercado" || FilCat == "Infantil")
            {
                caminhoEmpresa = "https://appmaragogi.com.br/Fotos/Empresas/Comprar/";
            }
            else if (FilCat == "Zoológico" || FilCat == "Vida Noturna" || FilCat == "Paintball" || FilCat == "Parque Aquático")
            {
                caminhoEmpresa = "https://appmaragogi.com.br/Fotos/Empresas/Diversao/";
            }
            else if (FilCat == "Resort" || FilCat == "Pousada" || FilCat == "Hostel" || FilCat == "Flat" || FilCat == "Condomínio" || FilCat == "Imóveis Temporada")
            {
                caminhoEmpresa = "https://appmaragogi.com.br/Fotos/Empresas/Dormir/";
            }
            else if (FilCat == "Eventos")
            {
                caminhoEmpresa = "https://appmaragogi.com.br/Fotos/Empresas/Eventos/";
            }
            else if (FilCat == "Agência de Viagens" || FilCat == "Transporte Alternativo" || FilCat == "Transfer" || FilCat == "Táxi" || FilCat == "Moto Táxi" || FilCat == "Aluguel de Carros" || FilCat == "Aluguel de Bicicleta")
            {
                caminhoEmpresa = "https://appmaragogi.com.br/Fotos/Empresas/Locomover/";
            }
            else if (FilCat == "Stand Up" || FilCat == "Mergulho" || FilCat == "Lanchas" || FilCat == "Jangada" || FilCat == "Gales" || FilCat == "Eco Turismo" || FilCat == "City Tours" || FilCat == "Caiaque" || FilCat == "Buggy" || FilCat == "Banana Boat")
            {
                caminhoEmpresa = "https://appmaragogi.com.br/Fotos/Empresas/Passeios/";
            }
            else if (FilCat == "Upa" || FilCat == "Ultrassom" || FilCat == "Laboratório" || FilCat == "Fisioterapia" || FilCat == "Dentista" || FilCat == "Consultório")
            {
                caminhoEmpresa = "https://appmaragogi.com.br/Fotos/Empresas/Saude/";
            }
            else if (FilCat == "Carpintaria" || FilCat == "Capotaria" || FilCat == "Auto Peças" || FilCat == "Auto Escola" || FilCat == "Academia" || FilCat == "Vidracaria" || FilCat == "Tatuagem" || FilCat == "Locutor" || FilCat == "Refrigeração" || FilCat == "Provedor Internet" || FilCat == "Pintor" || FilCat == "Petshop" || FilCat == "Pedreiro" || FilCat == "Moto Peças" || FilCat == "Lavanderia" || FilCat == "Lan House" || FilCat == "Gesseiro" || FilCat == "Gás" || FilCat == "Encanador" || FilCat == "Eletricista" || FilCat == "Educação" || FilCat == "Delicias Maragogi" || FilCat == "Contabilidade" || FilCat == "Construtora" || FilCat == "Conserto de Computador" || FilCat == "Conserto de Celular" || FilCat == "Cartório")
            {
                caminhoEmpresa = "https://appmaragogi.com.br/Fotos/Empresas/Servicos/";
            }

            for (int i = 1; i <= 3; i++)
            {
                var Fotos = string.Format("{0}{1}{2}{3}", caminhoEmpresa, filtroEmpresa.Replace(" ", ""), i, ".jpg");
                FotosEmpresa.Add(new FotosEstabelecimentos { Foto = Fotos });
            }
        }
    }
}
