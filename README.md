# 📊 Gerador de Gráficos Devexpress (Magic XPA + C#)

🔧 **Componente para geração dinâmica de gráficos** utilizando Devexpress no Magic XPA através de configuração XML.

## 🚀 Tecnologias Utilizadas
- .NET Framework 4.7+
- Devexpress WinForms (v19.1)
- Magic XPA 3.x+
- C# (para desenvolvimento do componente)

## 📋 Tipos de Gráficos Suportados

🥧 Gráfico de Pizza
📈 Gráfico de Linhas
🌄 Gráfico de Área
🍩 Gráfico Donut
✨ Gráfico de Dispersão

### 📊 Gráfico de Barras Verticais (Exemplo)
```xml
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
