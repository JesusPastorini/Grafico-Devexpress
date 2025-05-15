# 📊 Gerador de Gráficos Devexpress (Magic XPA + C#)

![Banner](https://via.placeholder.com/1200x400/2c3e50/ffffff?text=Gerador+de+Gráficos+Devexpress)  
*(Substitua por imagem real do seu componente em ação)*

## ⚠️ Requisitos de Licenciamento
🔑 **Licença Obrigatória**:  
Este componente requer uma **assinatura válida do Devexpress v19.1** para funcionamento.  
📌 **Versão Específica**: Devido a dependências internas, somente a versão **19.1.x** é compatível.

## 🚀 Tecnologias Utilizadas
- .NET Framework 4.7+
- Devexpress WinForms **v19.1 (licença paga obrigatória)**
- Magic XPA 3.x+
- C# (componente customizado)

## 🖼️ Galeria de Gráficos

### 📊 Gráfico de Barras Verticais
![Gráfico de Barras](https://via.placeholder.com/800x500/3498db/ffffff?text=Exemplo+Barras)  

### 🥧 Gráfico de Pizza
![Gráfico de Pizza](https://via.placeholder.com/600x600/e74c3c/ffffff?text=Exemplo+Pizza)  

### 📈 Gráfico de Linhas
![Gráfico de Linhas]([https://www.google.com/url?sa=i&url=https%3A%2F%2Fbr.pinterest.com%2Fpin%2F687221224384658306%2F&psig=AOvVaw3poQjnBHVhyijcYtKv3ytD&ust=1747414291678000&source=images&cd=vfe&opi=89978449&ved=0CBEQjRxqFwoTCIiZ6tX3pY0DFQAAAAAdAAAAABAE](https://pin.it/1TYjKlkD8))  

## 📋 Tipos de Gráficos Suportados

```xml
<!-- Exemplo Completo - Gráfico de Barras -->
<ChartConfig>
  <Title Text="Vendas Trimestrais 2024" Font="Segoe UI, 14pt, Bold" Color="#2c3e50" />
  <Size Width="1000" Height="600" />
  <Palette Name="Office2019Colorful" />
  
  <Series Type="Bar" Name="Vendas por Trimestre">
    <DataPoints>
      <Point Label="1º Trim" Value="12500" Color="#3498db" />
      <Point Label="2º Trim" Value="18700" Color="#2ecc71" />
      <Point Label="3º Trim" Value="15400" Color="#f1c40f" />
      <Point Label="4º Trim" Value="21000" Color="#e74c3c" />
    </DataPoints>
    <Appearance>
      <BarWidth>0.6</BarWidth>
      <Border Visible="true" Color="#7f8c8d" />
    </Appearance>
  </Series>
  
  <AxisX Title="Trimestres" GridLines="true" LabelAngle="0" />
  <AxisY Title="Valor (R$)" GridLines="true" 
         Range Min="0" Max="25000" Interval="5000" />
  
  <Legend Visible="true" Position="Bottom" HorizontalAlignment="Center" />
  <Tooltip Enabled="true" Format="R$ #,##0" />
</ChartConfig>
```

## 💰 Informações de Licenciamento

⚠️ **Requisito Obrigatório**  
Este componente necessita de **licença paga do DevExpress v19.1** para funcionamento.

### 📋 Detalhes Técnicos
| Componente | Versão Requerida |
|------------|------------------|
| DevExpress.Charts | 19.1.7 (exata) |
| DevExpress.Data | 19.1.x |
| DevExpress.Utils | 19.1.x |

### 🔑 Passos para Configuração
1. Adquirir licença no [site oficial](https://www.devexpress.com)
2. Instalar pacotes NuGet específicos:
   ```powershell
   Install-Package DevExpress.Charts -Version 19.1.7
