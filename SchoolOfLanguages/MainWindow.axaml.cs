using Avalonia.Controls;
using Microsoft.EntityFrameworkCore;
using SchoolOfLanguages.ListDan;
using SchoolOfLanguages.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolOfLanguages
{
    public partial class MainWindow : Window
    {
        private List<Client> allClients;//лист для базы(для сокращеия)
        private int page_Index = 0; // Отображаемая страница
        private int Number_of_elements_Page = 10; // Количество элементов что показываются на странице в данный момент (обновляется до 10/50/200/Всех)

        public MainWindow()
        {
            InitializeComponent();
            NextPage.Click += NextPage_Click;//Следующая страница
            PreviousPage.Click += PreviousPage_Click;//Прошлая страница
            TenPage.Click += TenPage_Click;//10 элементов
            FiveTenPage.Click += FiveTenPage_Click;//50 элементов
            TwoHundredPage.Click += TwoHundredPage_Click;//200 элементов
            AllPages.Click += AllPages_Click;//Все элементы

            // Обновление данных в листбоксе
            UpdateListBox();
            // Обновление информации по странице
            UpdatePageInfo();
        }

        private void UpdateListBox()
        {
            allClients = Helper.DataBase.Clients.ToList();
            var pagedClients = allClients.Skip(page_Index * Number_of_elements_Page).Take(Number_of_elements_Page).ToList();
            Clients.ItemsSource = pagedClients;
        }// Загрузка/Обновление данных
        private void UpdatePageInfo()
        {
            pageIndex.Text = (page_Index + 1).ToString();
            TotalPages.Text = Math.Ceiling((double)allClients.Count / Number_of_elements_Page).ToString();
        }// Обновление информации по странице (Вывод на экран происходит)
        private void NextPage_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if ((page_Index + 1) * Number_of_elements_Page < allClients.Count)
            {
                page_Index++;
                UpdateListBox();
                UpdatePageInfo();
            }
        }// Следующая страница
        private void PreviousPage_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (page_Index > 0)
            {
                page_Index--;
                UpdateListBox();
                UpdatePageInfo();
            }
        }// Предвидущая страница
        private void TenPage_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Number_of_elements_Page = 10;
            page_Index = 0;
            UpdateListBox();
            UpdatePageInfo();
        }// 10 элементов
        private void FiveTenPage_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Number_of_elements_Page = 50;
            page_Index = 0;
            UpdateListBox();
            UpdatePageInfo();
        }// 50 элементов
        private void TwoHundredPage_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Number_of_elements_Page = 200;
            page_Index = 0; 
            UpdateListBox();
            UpdatePageInfo();
        }// 200 элементов
        private void AllPages_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Number_of_elements_Page = allClients.Count; 
            page_Index = 0;
            UpdateListBox();
            UpdatePageInfo();
        }// Все элементы
    }
}