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
        private List<Client> allClients;//���� ��� ����(��� ���������)
        private int page_Index = 0; // ������������ ��������
        private int Number_of_elements_Page = 10; // ���������� ��������� ��� ������������ �� �������� � ������ ������ (����������� �� 10/50/200/����)

        public MainWindow()
        {
            InitializeComponent();
            NextPage.Click += NextPage_Click;//��������� ��������
            PreviousPage.Click += PreviousPage_Click;//������� ��������
            TenPage.Click += TenPage_Click;//10 ���������
            FiveTenPage.Click += FiveTenPage_Click;//50 ���������
            TwoHundredPage.Click += TwoHundredPage_Click;//200 ���������
            AllPages.Click += AllPages_Click;//��� ��������

            // ���������� ������ � ���������
            UpdateListBox();
            // ���������� ���������� �� ��������
            UpdatePageInfo();
        }

        private void UpdateListBox()
        {
            allClients = Helper.DataBase.Clients.ToList();
            var pagedClients = allClients.Skip(page_Index * Number_of_elements_Page).Take(Number_of_elements_Page).ToList();
            Clients.ItemsSource = pagedClients;
        }// ��������/���������� ������
        private void UpdatePageInfo()
        {
            pageIndex.Text = (page_Index + 1).ToString();
            TotalPages.Text = Math.Ceiling((double)allClients.Count / Number_of_elements_Page).ToString();
        }// ���������� ���������� �� �������� (����� �� ����� ����������)
        private void NextPage_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if ((page_Index + 1) * Number_of_elements_Page < allClients.Count)
            {
                page_Index++;
                UpdateListBox();
                UpdatePageInfo();
            }
        }// ��������� ��������
        private void PreviousPage_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (page_Index > 0)
            {
                page_Index--;
                UpdateListBox();
                UpdatePageInfo();
            }
        }// ����������� ��������
        private void TenPage_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Number_of_elements_Page = 10;
            page_Index = 0;
            UpdateListBox();
            UpdatePageInfo();
        }// 10 ���������
        private void FiveTenPage_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Number_of_elements_Page = 50;
            page_Index = 0;
            UpdateListBox();
            UpdatePageInfo();
        }// 50 ���������
        private void TwoHundredPage_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Number_of_elements_Page = 200;
            page_Index = 0; 
            UpdateListBox();
            UpdatePageInfo();
        }// 200 ���������
        private void AllPages_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Number_of_elements_Page = allClients.Count; 
            page_Index = 0;
            UpdateListBox();
            UpdatePageInfo();
        }// ��� ��������
    }
}