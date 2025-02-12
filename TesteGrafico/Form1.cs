using System;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Xml.Linq;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using System.Collections.Generic;

namespace TesteGrafico
{
    //----------2.2
    public partial class Form1 : Form
    {
        private ChartControl chartControl;
        private ViewType tipoGrafico;
        private XElement xml2;

        private bool titleVisible;
        private string TituloGrafico;
        private string fonte;
        private float TamanhoFonteTitulo;
        private FontStyle EstiloFonteTitulo;
        private Color corTitulo;

        private bool existeSubtituloX;
        private bool existeSubtituloY;
        private string subtitleX;
        private string subtitleY;
        private string fontX;
        private string fontY;
        private FontStyle styleSubtitleX;
        private FontStyle styleSubtitleY;
        private int sizeX;
        private int sizeY;
        private Color corSubtituloX;
        private Color corSubtituloY;

        private bool gradeDiagram;
        private bool visivelEixoX;
        private bool visivelEixoY;
        private string formatoTexto;
        private double margemDadosEixoX;
        private int espessuraEixoX;
        private int espessuraEixoY;
        private Color corX;
        private Color corY;
        private int TamanhoFontX;
        private int TamanhoFontY;
        private int rotacaoX;
        private int rotacaoY;
        private bool borderPlotagem;
        private bool orientacaoXY;

        private Color backGroundColor;
        private Color backGroundDiagram;
        private bool BackgroundTransparent;

        private bool removerFundoLegenda;
        private bool legendaVisivel;
        private bool legendHorizontal;
        private string fontLegenda;
        private float sizeFonteLegenda;
        private FontStyle styleFonteLegenda;
        private int porcentagemHorizontal;
        private int opacidadeLegenda;
        private Color corLegenda;
        private Color corTextLegenda;
        private int legendMarkerX;
        private int legendMarkerY;
        private string positionHorizontal;
        private string positionVertical;
        private double tamanhoBloco;

        private string Picture;
        private string positionRotulo;
        private Color colorBackground;
        private bool borderRotulo;
        private Color textColor;
        private bool borderPie;
        private Color colorBorderPie;
        private int tamanhoBorderPie;
        private string pictureLegend;

        private string fontLabelPie;
        private int SizeLabelPie;
        private FontStyle styeleLabelPie;

        private IEnumerable<XElement> seriesElements;
        private IEnumerable<XElement> dataElements;

        public Form1()
        {
            InitializeComponent();
            try
            {
                CarregarConfiguracoesDeXml();
                GeraGrafico();
                PopularGrafico();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao inicializar: {ex.Message}");
            }
        }

        private void CarregarConfiguracoesDeXml()
        {
            try
            {
                xml2 = XElement.Load("C:\\Users\\ADDERI\\Desktop\\Grafico3\\xmlGrafico3LineArea.xml");
                var extractXml = CarregaPadraoXml(xml2);
                //MessageBox.Show(extractXml.FonteLegenda);

                //seriesElements = xml2.Element("Series")?.Elements("Serie");               
                //dataElements = xml2.Element("Data")?.Elements("Line");

                // Carregar o tipo de gráfico do XML--
                //var graficoElement = xml2.Element("Style");
                //var graficoType = graficoElement.Element("Type").Value;
                //BackgroundTransparent = bool.Parse(graficoElement.Element("BackgroundTransparent").Value);
                //backGroundColor = ColorTranslator.FromHtml(graficoElement.Element("BackgroundColor").Value);
                //if (!Enum.TryParse(graficoType, true, out tipoGrafico))
                //{
                //    tipoGrafico = ViewType.Bar;
                //}

                // Carregar título
                var tituloElement = xml2.Element("Title");
                titleVisible = bool.Parse(tituloElement.Element("Visible").Value);
                TituloGrafico = tituloElement.Element("Text").Value;
                fonte = tituloElement.Element("Font").Value;
                TamanhoFonteTitulo = float.Parse(tituloElement.Element("Size").Value);
                // Converte a string para FontStyle
                EstiloFonteTitulo = (System.Drawing.FontStyle)Enum.Parse(typeof(System.Drawing.FontStyle), tituloElement.Element("Style").Value);
                corTitulo = ColorTranslator.FromHtml(tituloElement.Element("Color").Value);
                
                // Carregar legenda--
                var legendaElement = xml2.Element("Legend");
                legendaVisivel = bool.Parse(legendaElement.Element("Visible").Value);
                fontLegenda = legendaElement.Element("Font").Value;
                sizeFonteLegenda = float.Parse(legendaElement.Element("Size").Value);
                styleFonteLegenda = (System.Drawing.FontStyle)Enum.Parse(typeof(System.Drawing.FontStyle), legendaElement.Element("Style").Value);
                corTextLegenda = ColorTranslator.FromHtml(legendaElement.Element("Color").Value);

                opacidadeLegenda = int.Parse(legendaElement.Element("BackgroundOpacity").Value);
                removerFundoLegenda = bool.Parse(legendaElement.Element("BackgroundTransparent").Value);
                corLegenda = ColorTranslator.FromHtml(legendaElement.Element("BackgroundColor").Value);
                legendHorizontal = bool.Parse(legendaElement.Element("DirectionHorizontal").Value);
                porcentagemHorizontal = int.Parse(legendaElement.Element("PercentageHorizontal").Value);
                positionHorizontal = legendaElement.Element("PositionHorizontal").Value;
                positionVertical = legendaElement.Element("PositionVertical").Value;
                legendMarkerX = int.Parse(legendaElement.Element("MarkerWidth").Value);
                legendMarkerY = int.Parse(legendaElement.Element("MarkerHeight").Value);
                pictureLegend = (legendaElement != null && legendaElement.Element("Picture") != null) ? legendaElement.Element("Picture").Value : "{A}";

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar configurações do XML: {ex.Message}");
            }
        }
        private void GeraGrafico()
        {
            try
            {
                chartControl = new ChartControl
                {
                    Dock = DockStyle.Fill
                };

                Controls.Add(chartControl);

                if (titleVisible)
                {
                    var titleElement = new ChartTitle
                    {
                        Text = TituloGrafico,
                        Font = new System.Drawing.Font(fonte, TamanhoFonteTitulo, EstiloFonteTitulo),
                        TextColor = corTitulo
                    };
                    chartControl.Titles.Add(titleElement);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao gerar o gráfico: {ex.Message}");
            }
        }

        private void PopularGrafico()
        {
            try
            {
                xml2 = XElement.Load("C:\\Users\\ADDERI\\Desktop\\Grafico3\\xmlGrafico3LineArea.xml");
                var extractXml = CarregaPadraoXml(xml2);
                chartControl.Series.Clear();                            

                foreach (var serieElement in extractXml.SeriesElements)
                {
                    // Criação da série
                    var series = new Series(serieElement.Element("Text").Value, extractXml.TipoGrafico);

                    // Configurar a cor da série
                    series.View.Color = ColorTranslator.FromHtml(serieElement.Element("Color").Value);

                    // Configurar rótulo da série
                    var labelElement = serieElement.Element("Label");
                    if (labelElement != null && bool.TryParse(labelElement.Element("Visible")?.Value, out bool isVisible))
                    {
                        if (isVisible)
                        {
                            // Configurar propriedades comuns do rótulo
                            var label = series.Label as DevExpress.XtraCharts.SeriesLabelBase;
                            if (label != null)
                            {
                                series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

                                label.TextColor = ColorTranslator.FromHtml(labelElement.Element("Color").Value);

                                var fontName = labelElement.Element("Font").Value;
                                var fontSize = float.Parse(labelElement.Element("Size").Value);
                                var fontStyle = (System.Drawing.FontStyle)Enum.Parse(typeof(System.Drawing.FontStyle), labelElement.Element("Style").Value);
                                label.Font = new System.Drawing.Font(fontName, fontSize, fontStyle);

                                var textPattern = labelElement.Element("Picture").Value;
                                label.TextPattern = textPattern;

                                // Configurar fundo do rótulo
                                var isBackgroundTransparent = bool.Parse(labelElement.Element("BackgroundTransparent").Value);
                                if (isBackgroundTransparent)
                                {
                                    label.BackColor = System.Drawing.Color.Transparent;
                                    label.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
                                }
                                else
                                {
                                    label.BackColor = ColorTranslator.FromHtml(labelElement.Element("BackgroundColor").Value);
                                    label.Border.Visibility = DevExpress.Utils.DefaultBoolean.True;
                                }
                                // Configurações específicas por tipo de gráfico
                                var positionValue = labelElement.Element("Position").Value;
                                var plotArea = xml2.Element("Area");                                
                                if (series.View is BarSeriesView barView)
                                {
                                   // series.Points.Clear();
                                    //series.LegendTextPattern = pictureLegend;// Aplicar o padrão de legenda somente para PieSeries
                                    var blockSize = double.Parse(plotArea.Element("BlockSize").Value);
                                    barView.BarWidth = blockSize;

                                   
                                    if (label is DevExpress.XtraCharts.BarSeriesLabel barLabel)
                                    {
                                        barLabel.Position = (BarSeriesLabelPosition)Enum.Parse(typeof(BarSeriesLabelPosition), positionValue);
                                    }
                                }
                                else if (series.View is LineSeriesView)
                                {
                                    var view = (LineSeriesView)series.View; // Cast explícito
                                    var exibeMarker = bool.Parse(labelElement.Element("Marker").Element("Visible").Value);
                                    // Marcador das séries
                                    if (exibeMarker)
                                    {
                                        var marker = labelElement.Element("Marker");
                                        view.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;                                       
                                        view.LineMarkerOptions.Size = int.Parse(marker.Element("Size").Value);
                                        view.LineMarkerOptions.Color = ColorTranslator.FromHtml(marker.Element("Color").Value);
                                        string markerKind = marker.Element("Kind").Value;
                                        view.LineMarkerOptions.Kind = (MarkerKind)Enum.Parse(typeof(MarkerKind), markerKind);// Circle, Square, Diamond, Triangle, InvertedTriangle, Cross, Plus.
                                        view.LineMarkerOptions.BorderVisible = bool.Parse(marker.Element("BorderVisible").Value);
                                        view.LineMarkerOptions.BorderColor = ColorTranslator.FromHtml(marker.Element("BorderColor").Value); ;
                                    }

                                    if (label is DevExpress.XtraCharts.PointSeriesLabel pointLabel)
                                    {
                                        pointLabel.Position = (PointLabelPosition)Enum.Parse(typeof(PointLabelPosition), positionValue);
                                    }

                                }                               
                            }
                        }
                        else
                        {
                            // Desativar rótulo
                            series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
                        }
                    }
                    else
                    {
                        // Caso o elemento não exista ou não seja configurado como visível
                        series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
                    }


                    // Adicionar série ao gráfico
                    chartControl.Series.Add(series);
                }

                // Configurar os dados
                if (extractXml.DataElements != null)
                {
                    foreach (var lineElement in extractXml.DataElements)
                    {
                        var dataPoint = lineElement.Element("Text").Value;
                        var seriesData = lineElement.Element("Series").Elements("Serie")
                            .Select(s => double.Parse(s.Value))
                            .ToList();

                        if (seriesData != null)
                        {
                            for (int i = 0; i < chartControl.Series.Count && i < seriesData.Count; i++)
                            {
                                chartControl.Series[i].Points.Add(new SeriesPoint(dataPoint, seriesData[i]));
                            }
                        }
                    }
                }
                
                var seriesL = chartControl.Series[0];
                // Configurar os dados e cores para o gráfico de pizza
                if (tipoGrafico == ViewType.Pie || tipoGrafico == ViewType.Doughnut)
                {                  
                    // Pie
                    var seriePie = xml2.Element("Series").Element("Serie");
                    var pieLabel = seriePie.Element("Label");

                    positionRotulo = pieLabel.Element("Position").Value;
                    borderRotulo = bool.Parse(pieLabel.Element("Border").Value);

                    fontLabelPie = pieLabel.Element("Font").Value;
                    SizeLabelPie = int.Parse(pieLabel.Element("Size").Value);
                    styeleLabelPie = (System.Drawing.FontStyle)Enum.Parse(typeof(System.Drawing.FontStyle), pieLabel.Element("Style").Value);

                    textColor = ColorTranslator.FromHtml(pieLabel.Element("Color").Value);
                    colorBackground = ColorTranslator.FromHtml(pieLabel.Element("BackgroundColor").Value);
                    Picture = pieLabel.Element("Picture").Value;

                    borderPie = bool.Parse(seriePie.Element("Border").Value);
                    colorBorderPie = ColorTranslator.FromHtml(seriePie.Element("Color").Value);
                    tamanhoBorderPie = int.Parse(seriePie.Element("LineWidth").Value);

                    if (seriesL != null && extractXml.DataElements != null)
                    {
                        // Ajustar a visualização
                        var pieSeriesView = seriesL.View as PieSeriesView;

                        // Limpar os pontos existentes na série para evitar duplicações
                        seriesL.Points.Clear();
                        // Limpar pontos deslocados previamente
                        pieSeriesView.ExplodedPoints.Clear();
                        foreach (var lineElement in extractXml.DataElements)
                        {
                            seriesL.Label.TextPattern = Picture;

                            // Modifica os rótulos: Radial, Tangent, TwoColumns, Outside, Inside
                            PieSeriesLabel label = seriesL.Label as PieSeriesLabel;
                            label.Position = (PieSeriesLabelPosition)Enum.Parse(typeof(PieSeriesLabelPosition), positionRotulo);
                            label.BackColor = colorBackground; // Remove a cor de fundo do rótulo
                            label.Border.Visible = borderRotulo; // Remove as bordas do rótulo
                            label.TextColor = textColor;
                            label.Font = new Font(fontLabelPie, SizeLabelPie, styeleLabelPie);

                            // Obter o valor do elemento <Text> e o primeiro <Serie> em <Series>
                            var dataPoint = lineElement.Element("Text")?.Value;
                            var serieElement = lineElement.Element("Series")?.Element("Serie")?.Value;
                            var sliceColor = lineElement.Element("Color")?.Value;
                            var movieSliceValue = lineElement.Element("MovieSlice")?.Value;

                            if (!string.IsNullOrEmpty(dataPoint) && !string.IsNullOrEmpty(serieElement) && !string.IsNullOrEmpty(sliceColor))
                            {
                                // Converter os valores para os tipos apropriados
                                double value = Convert.ToDouble(serieElement.Trim());
                                int movieSlice = string.IsNullOrEmpty(movieSliceValue) ? 0 : int.Parse(movieSliceValue);                 

                                // Criar o ponto da série
                                var point = new SeriesPoint(dataPoint, value);

                                // Atribuir a cor da fatia a partir de <Color>
                                point.Color = ColorTranslator.FromHtml(sliceColor);

                                seriesL.Points.Add(point);

                            // Configurar o deslocamento para a fatia, se <MovieSlice> for maior que 0
                            if (movieSlice > 0)
                            {
                                // Adicionar o índice da fatia deslocada
                                pieSeriesView.ExplodedPoints.Add(point);
                            }
                            }
                        }
                        // Configurar bordas
                        if (borderPie)
                        {
                            pieSeriesView.Border.Visibility = DevExpress.Utils.DefaultBoolean.True;
                            pieSeriesView.Border.Color = colorBorderPie;
                            pieSeriesView.Border.Thickness = tamanhoBorderPie;
                        }
                    }
                }
                else
                {
                    // Subtitulo X--
                    var subtituloXElement = xml2.Element("TitleX");
                    existeSubtituloX = bool.Parse(subtituloXElement.Element("Visible").Value);
                    subtitleX = subtituloXElement.Element("Text").Value;
                    fontX = subtituloXElement.Element("Font").Value;
                    sizeX = int.Parse(subtituloXElement.Element("Size").Value);
                    styleSubtitleX = (System.Drawing.FontStyle)Enum.Parse(typeof(System.Drawing.FontStyle), subtituloXElement.Element("Style").Value);
                    corSubtituloX = ColorTranslator.FromHtml(subtituloXElement.Element("Color").Value);
                    //Eixo X--
                    var elementoEixoX = xml2.Element("LabelX");
                    visivelEixoX = bool.Parse(elementoEixoX.Element("Visible").Value);
                    TamanhoFontX = int.Parse(elementoEixoX.Element("Size").Value);
                    corX = ColorTranslator.FromHtml(elementoEixoX.Element("Color").Value);
                    espessuraEixoX = int.Parse(elementoEixoX.Element("LineWidth").Value);
                    margemDadosEixoX = double.Parse(elementoEixoX.Element("LeftMargin").Value);

                    // Subtitulo Y--
                    var subtituloYElement = xml2.Element("TitleY");
                    existeSubtituloY = bool.Parse(subtituloYElement.Element("Visible").Value);
                    subtitleY = subtituloYElement.Element("Text").Value;
                    fontY = subtituloYElement.Element("Font").Value;
                    sizeY = int.Parse(subtituloYElement.Element("Size").Value);
                    styleSubtitleY = (System.Drawing.FontStyle)Enum.Parse(typeof(System.Drawing.FontStyle), subtituloYElement.Element("Style").Value);
                    corSubtituloY = ColorTranslator.FromHtml(subtituloYElement.Element("Color").Value);
                    //Eixo Y--
                    var elementoEixoY = xml2.Element("LabelY");
                    visivelEixoY = bool.Parse(elementoEixoY.Element("Visible").Value);
                    TamanhoFontY = int.Parse(elementoEixoY.Element("Size").Value);
                    corY = ColorTranslator.FromHtml(elementoEixoY.Element("Color").Value);
                    espessuraEixoY = int.Parse(elementoEixoY.Element("LineWidth").Value);
                    formatoTexto = elementoEixoY.Element("Picture").Value;

                    // Plotagem--
                    var AreaPlotagem = xml2.Element("Area");
                    gradeDiagram = bool.Parse(AreaPlotagem.Element("Grade").Value);
                    borderPlotagem = bool.Parse(AreaPlotagem.Element("Border").Value);
                    orientacaoXY = bool.Parse(AreaPlotagem.Element("OrientationXY").Value);
                    rotacaoX = int.Parse(AreaPlotagem.Element("RotationX").Value);
                    rotacaoY = int.Parse(AreaPlotagem.Element("RotationY").Value);
                    backGroundDiagram = ColorTranslator.FromHtml(AreaPlotagem.Element("Color").Value);
                    
                    // **Diagrama Eixo X e Y
                    XYDiagram diagram = chartControl.Diagram as XYDiagram;
                    if (diagram != null)
                    {
                        // -----------New-----------------
                        // Configurações para escala de tempo 1
                        //diagram.AxisX.DateTimeScaleOptions.ScaleMode = ScaleMode.Manual;
                        //diagram.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Month; // Unidade: Mês
                        //diagram.AxisX.DateTimeScaleOptions.GridSpacing = 3; // Espaçamento entre os pontos

                        // Configurações para escala numerica 2
                        //diagram.AxisX.NumericScaleOptions.ScaleMode = ScaleMode.Manual;
                        //diagram.AxisX.NumericScaleOptions.GridSpacing = 4; // Define o espaçamento entre rótulos

                        diagram.AxisY.Label.TextPattern = formatoTexto;
                        diagram.AxisX.Title.Text = subtitleX;
                        diagram.AxisX.Title.Visibility = existeSubtituloX
                            ? DevExpress.Utils.DefaultBoolean.True
                            : DevExpress.Utils.DefaultBoolean.False;
                        diagram.AxisX.Title.TextColor = corSubtituloX;
                        diagram.AxisX.Title.Font = new System.Drawing.Font(fontX, sizeX, styleSubtitleX);
                        // Margens do eixo X para evitar sobreposição com a área de plotagem

                        diagram.AxisX.WholeRange.SideMarginsValue = margemDadosEixoX; // Ajustar margens laterais

                        diagram.AxisX.Thickness = espessuraEixoX; // Espessura da linha do eixo
                        diagram.AxisY.Thickness = espessuraEixoY;                                      

                        diagram.AxisX.Visible = visivelEixoX; // Esconde o eixo X, incluindo a linha
                        diagram.AxisY.Visible = visivelEixoY;                 

                        diagram.AxisX.GridLines.Visible = gradeDiagram;

                        diagram.AxisX.Label.TextColor = corX;
                        diagram.AxisX.Label.Font = new System.Drawing.Font(fonte, TamanhoFontX);
                        diagram.AxisX.Label.Angle = rotacaoX;

                        diagram.AxisY.Title.Text = subtitleY;
                        diagram.AxisY.Title.Visibility = existeSubtituloY
                            ? DevExpress.Utils.DefaultBoolean.True
                            : DevExpress.Utils.DefaultBoolean.False;
                        diagram.AxisY.Title.TextColor = corSubtituloY;
                        diagram.AxisY.Title.Font = new System.Drawing.Font(fontY, sizeY, styleSubtitleY);

                        diagram.AxisY.GridLines.Visible = gradeDiagram;

                        diagram.AxisY.Label.TextColor = corY;
                        diagram.AxisY.Label.Font = new System.Drawing.Font(fonte, TamanhoFontY);
                        diagram.AxisY.Label.Angle = rotacaoY;

                        diagram.DefaultPane.BorderVisible = borderPlotagem;
                        diagram.DefaultPane.BackColor = backGroundDiagram;
                        diagram.Rotated = orientacaoXY;
                    }
                }

                // **Background
                if (extractXml.BackgroundTransparent)
                {
                    chartControl.BackColor = System.Drawing.Color.Transparent;
                    chartControl.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.False; // Remove borda, se houver
                    chartControl.RuntimeHitTesting = true; // Garantir que o fundo seja transparente para interações
                } else
                {
                    chartControl.BackColor = extractXml.BackgroundColor;
                }

                // **Legenda**
                
                chartControl.Legend.Visibility = legendaVisivel ? DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
                if (legendHorizontal) {
                    chartControl.Legend.Direction = LegendDirection.LeftToRight; // Disposição lado a lado
                    chartControl.Legend.MaxHorizontalPercentage = porcentagemHorizontal; // Ocupa no máximo 50% da largura do controle
                }
                chartControl.Legend.TextColor = corTextLegenda;

                // Left, Center, Right, RightOutside, LeftOutside 
                chartControl.Legend.AlignmentHorizontal = (LegendAlignmentHorizontal)Enum.Parse(typeof(LegendAlignmentHorizontal), positionHorizontal);
                // Top, Center, Bottom, BottomOutside, TopOutside
                chartControl.Legend.AlignmentVertical = (LegendAlignmentVertical)Enum.Parse(typeof(LegendAlignmentVertical), positionVertical);

                if (removerFundoLegenda)
                {
                    chartControl.Legend.BackColor = System.Drawing.Color.Transparent;
                    chartControl.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.False; // Remover bordas
                }
                else
                {
                    chartControl.Legend.BackColor = System.Drawing.Color.FromArgb(opacidadeLegenda, corLegenda.R, corLegenda.G, corLegenda.B);
                    chartControl.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.True; // Adicionar bordas
                }

                chartControl.Legend.MarkerSize = new System.Drawing.Size(legendMarkerX, legendMarkerY);

                chartControl.Legend.Font = new System.Drawing.Font(fontLegenda, sizeFonteLegenda, styleFonteLegenda);

                seriesL.LegendTextPattern = pictureLegend;// Aplicar o padrão de legenda somente para PieSeries
               

                // **ToolTip
                chartControl.ToolTipEnabled = DevExpress.Utils.DefaultBoolean.True;
            }
            catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao popular o gráfico: {ex.Message}");
                }
        }
        private static DashboardConfig CarregaPadraoXml(XElement xml2)
        {
            var config = new DashboardConfig();

            // Carregar Series e Data
            config.SeriesElements = xml2.Element("Series")?.Elements("Serie").ToList();
            config.DataElements = xml2.Element("Data")?.Elements("Line").ToList();

            // Carregar o tipo de gráfico do XML
            var graficoElement = xml2.Element("Style");
            if (graficoElement != null)
            {
                config.TipoGrafico = Enum.TryParse(graficoElement.Element("Type")?.Value, true, out ViewType tipo)
                    ? tipo
                    : ViewType.Bar;
                MessageBox.Show($"entrou");
                config.BackgroundTransparent = bool.Parse(graficoElement.Element("BackgroundTransparent")?.Value ?? "false");
                config.BackgroundColor = ColorTranslator.FromHtml(graficoElement.Element("BackgroundColor")?.Value);
            }

            // Carregar título
            var tituloElement = xml2.Element("Title");
            if (tituloElement != null)
            {
                config.TituloGrafico = tituloElement.Element("Text")?.Value;
                config.TitleVisible = bool.Parse(tituloElement.Element("Visible")?.Value ?? "false");
                config.FonteTitulo = tituloElement.Element("Font")?.Value;
                config.TamanhoFonteTitulo = float.Parse(tituloElement.Element("Size")?.Value ?? "0");
                config.EstiloFonteTitulo = Enum.TryParse(tituloElement.Element("Style")?.Value, out FontStyle estilo)
                    ? estilo
                    : FontStyle.Regular;
                config.CorTitulo = ColorTranslator.FromHtml(tituloElement.Element("Color")?.Value);
            }

            // Carregar legenda
            var legendaElement = xml2.Element("Legend");
            if (legendaElement != null)
            {
                config.LegendaVisivel = bool.Parse(legendaElement.Element("Visible")?.Value ?? "false");
                config.FonteLegenda = legendaElement.Element("Font")?.Value;
                config.SizeFonteLegenda = float.Parse(legendaElement.Element("Size")?.Value ?? "0");
                config.StyleFonteLegenda = Enum.TryParse(legendaElement.Element("Style")?.Value, out FontStyle estiloLegenda)
                    ? estiloLegenda
                    : FontStyle.Regular;
                config.CorTextoLegenda = ColorTranslator.FromHtml(legendaElement.Element("Color")?.Value);

                config.OpacidadeLegenda = int.Parse(legendaElement.Element("BackgroundOpacity")?.Value ?? "0");
                config.RemoverFundoLegenda = bool.Parse(legendaElement.Element("BackgroundTransparent")?.Value ?? "false");
                config.CorLegenda = ColorTranslator.FromHtml(legendaElement.Element("BackgroundColor")?.Value);
                config.LegendHorizontal = bool.Parse(legendaElement.Element("DirectionHorizontal")?.Value ?? "false");
                config.PorcentagemHorizontal = int.Parse(legendaElement.Element("PercentageHorizontal")?.Value ?? "0");
                config.PositionHorizontal = legendaElement.Element("PositionHorizontal")?.Value;
                config.PositionVertical = legendaElement.Element("PositionVertical")?.Value;
                config.LegendMarkerX = int.Parse(legendaElement.Element("MarkerWidth")?.Value ?? "0");
                config.LegendMarkerY = int.Parse(legendaElement.Element("MarkerHeight")?.Value ?? "0");
                config.PictureLegend = legendaElement.Element("Picture")?.Value;
            }

            return config;
        }

        // Classe para encapsular os dados do XML
        private class DashboardConfig
        {
            // Series e Data
            public List<XElement> SeriesElements { get; set; }
            public List<XElement> DataElements { get; set; }

            // Tipo de Gráfico
            public ViewType TipoGrafico { get; set; }
            public bool BackgroundTransparent { get; set; }
            public Color BackgroundColor { get; set; }

            // Título
            public string TituloGrafico { get; set; }
            public bool TitleVisible { get; set; }
            public string FonteTitulo { get; set; }
            public float TamanhoFonteTitulo { get; set; }
            public FontStyle EstiloFonteTitulo { get; set; }
            public Color CorTitulo { get; set; }

            // Legenda
            public bool LegendaVisivel { get; set; }
            public string FonteLegenda { get; set; }
            public float SizeFonteLegenda { get; set; }
            public FontStyle StyleFonteLegenda { get; set; }
            public Color CorTextoLegenda { get; set; }
            public int OpacidadeLegenda { get; set; }
            public bool RemoverFundoLegenda { get; set; }
            public Color CorLegenda { get; set; }
            public bool LegendHorizontal { get; set; }
            public int PorcentagemHorizontal { get; set; }
            public string PositionHorizontal { get; set; }
            public string PositionVertical { get; set; }
            public int LegendMarkerX { get; set; }
            public int LegendMarkerY { get; set; }
            public string PictureLegend { get; set; }
        }
    }
}