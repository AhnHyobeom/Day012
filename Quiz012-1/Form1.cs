using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace Quiz012_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static byte[,] inImage, outImage;
        static int inH, inW, outH, outW;
        static string fileName;
        static Bitmap paper;//그림을 콕콕 찍을 종이
        static bool mouseYN = false;
        static int mouseSX, mouseSY, mouseEX, mouseEY;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.O:
                        openImage();
                        break;
                    case Keys.S:
                        saveImage();
                        break;
                }
            }
            if (e.Alt)
            {
                switch (e.KeyCode)
                {
                    case Keys.E:
                        embossImage();
                        break;
                    case Keys.B:
                        blurrImage();
                        break;
                    case Keys.S:
                        sharpImage();
                        break;
                    case Keys.M:
                        medianFilter();
                        break;
                }
            }
        }

        private void 열기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openImage();
        }

        private void 동일이미지ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            equal_image();
        }

        private void 흑백이미지ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bwImage();
        }

        private void 반전이미지ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reverseImage();
        }

        private void 밝게어둡게ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            brightImage();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void 감마이미지ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void 확대ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sizeUpImage();
        }

        private void 축소ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sizeDownImage();
        }

        private void 회전ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rotateImage();
        }

        private void 영역처리ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 모자이크ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 엠보싱ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            embossImage();
        }

        private void 블러링ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            blurrImage();
        }

        private void 샤프닝ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sharpImage();
        }

        private void erosionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            morphology mp = new morphology();
            if (mp.ShowDialog() == DialogResult.OK) //grayscale image
            {
                erosionGray();
            }
            else //bwImage
            {
                erosionBW();
            }
        }

        private void dilationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            morphology mp = new morphology();
            if (mp.ShowDialog() == DialogResult.OK) //grayscale image
            {
                dilationGray();
            }
            else//bwImage
            {
                dilationBW();
            }
        }

        private void openingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            morphology mp = new morphology();
            if(mp.ShowDialog() == DialogResult.OK) //grayscale image
            {
                openingGray();
            } else//bwImage
            {
                openingBW();
            }
        }

        private void closingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            morphology mp = new morphology();
            if (mp.ShowDialog() == DialogResult.OK) //grayscale image
            {
                closingGray();
            }
            else //bwImage
            {
                closingBW();
            }
            
        }

        private void 모폴로지ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 스트레칭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            histoImage();
        }

        private void 엔드인탐색ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            endInSearch();
        }

        private void 평활화ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            histEqualized();
        }

        private void 미디언필터ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            medianFilter();
        }

        private void 저장ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveImage();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (!mouseYN)
            {
                return;
            }
            mouseEX = e.X;
            mouseEY = e.Y;

            if (mouseSX > mouseEX)
            {
                int tmp = mouseSX;
                mouseSX = mouseEX;
                mouseEX = tmp;
            }
            if (mouseSY > mouseEY)
            {
                int tmp = mouseSY;
                mouseSY = mouseEY;
                mouseEY = tmp;
            }

            reverseImage();
            mouseYN = false;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (!mouseYN)
            {
                return;
            }
            mouseSX = e.X; mouseSY = e.Y;
        }
        private void 반전마우스로지정ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mouseYN = true;
        }

        //공통 함수부
        void openImage()
        {
            OpenFileDialog ofd = new OpenFileDialog(); //객체 생성
            ofd.ShowDialog();
            fileName = ofd.FileName;
            BinaryReader br = new BinaryReader(File.Open(fileName, FileMode.Open));
            // 파일 크기 알아내기
            long fsize = new FileInfo(fileName).Length;
            // 중요! 입력이미지의 높이, 폭 알아내기
            inH = inW = (int)Math.Sqrt(fsize);
            inImage = new byte[inH, inW]; // 메모리 할당
            for (int i = 0; i < inH; i++)
            {
                for (int j = 0; j < inW; j++)
                {
                    inImage[i, j] = br.ReadByte();
                }
            }
            br.Close();

            equal_image();
        }

        void displayImage()
        {
            //벽, 게시판, 종이 크기 조절
            paper = new Bitmap(outH, outW);//종이
            pictureBox1.Size = new Size(outH, outW);//캔버스
            this.Size = new Size(outH + 100, outW + 150);//벽

            Color pen;
            for (int i = 0; i < outH; i++)
            {
                for (int j = 0; j < outW; j++)
                {
                    byte data = outImage[i, j];//잉크(색상값)
                    pen = Color.FromArgb(data, data, data);//팬에 잉크 묻히기
                    paper.SetPixel(j, i, pen);//종이에 찍기
                }
            }
            pictureBox1.Image = paper;//게시판에 종이를 붙이기

            toolStripStatusLabel1.Text = outH.ToString() + " x " + outW.ToString() + " " + fileName;
        }

        private void saveImage()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "raw";
            sfd.Filter = "로우 이미지(*.raw) | *.raw";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string saveFname = sfd.FileName;
                BinaryWriter bw = new BinaryWriter(File.Open(saveFname, FileMode.Create));
                for (int i = 0; i < outH; i++)
                    for (int k = 0; k < outW; k++)
                        bw.Write(outImage[i, k]);
                bw.Close();
            }
        }

        //영상 처리 함수부
        void equal_image()
        {
            if (inImage == null)
                return;
            outH = inH;
            outW = inW;
            outImage = new byte[outH, outW];
            for (int i = 0; i < inH; i++)
            {
                for (int j = 0; j < inW; j++)
                {
                    outImage[i, j] = inImage[i, j];
                }
            }
            displayImage();
        }

        void reverseImage()
        {
            if (inImage == null)
            {
                return;
            }
            outH = inH;
            outW = inW;
            outImage = new byte[outH, outW];

            if (!mouseYN)
            {
                mouseSX = 0; mouseEX = inH;
                mouseSY = 0; mouseEY = inW;
            }

            for (int i = 0; i < inH; i++)
            {
                for (int j = 0; j < inW; j++)
                {
                    if ((mouseSX <= j && j <= mouseEX) && (mouseSY <= i && i <= mouseEY))
                    {
                        outImage[i, j] = (byte)(255 - inImage[i, j]);
                    }
                    else
                    {
                        outImage[i, j] = inImage[i, j];
                    }
                }
            }
            displayImage();
        }


        void bwImage()
        {
            if (inImage == null)
            {
                return;
            }
            outH = inH;
            outW = inW;
            outImage = new byte[outH, outW];
            for (int i = 0; i < inH; i++)
            {
                for (int k = 0; k < inW; k++)
                {
                    if(inImage[i, k] > 127)
                    {
                        outImage[i, k] = 255;
                    } else
                    {
                        outImage[i, k] = 0;
                    }
                }
            }
            displayImage();
        }

        double getValue()
        {
            subform sub = new subform();//서브폼 준비
            if (sub.ShowDialog() == DialogResult.Cancel)
            {
                return 0.0;
            }
            double value = (double)sub.numUp_value.Value;
            return value;
        }

        void brightImage()
        {
            if (inImage == null)
            {
                return;
            }
            int value = (int)getValue();
            outH = inH;
            outW = inW;
            outImage = new byte[outH, outW];
            for (int i = 0; i < inH; i++)
            {
                for (int j = 0; j < inW; j++)
                {
                    if(inImage[i,j] + value > 255)
                    {
                        outImage[i, j] = 255;
                    } else if(inImage[i, j] + value < 0)
                    {
                        outImage[i, j] = 0;
                    } else
                    {
                        outImage[i, j] = (byte)(inImage[i, j] + value);
                    }
                }
            }
            displayImage();
        }

        void sizeUpImage()
        {//확대 알고리즘
            if (inImage == null)
            {
                return;
            }
            int mul = (int)getValue();
            outH = inH * mul;
            outW = inW * mul;
            outImage = new byte[outH, outW];
            for (int i = 0; i < outH; i++)
            {
                for (int j = 0; j < outW; j++)
                {
                    outImage[i, j] = inImage[i / mul, j / mul];
                }
            }
            displayImage();
        }
        void sizeDownImage()
        {//축소 알고리즘
            if (inImage == null)
            {
                return;
            }
            int div;
            div = (int)getValue();
            outH = inH / div;
            outW = inW / div;
            outImage = new byte[outH, outW];
            int sum;
            for (int i = 0; i < outH; i++)
            {//평균값으로 계산
                for (int j = 0; j < outW; j++)
                {
                    sum = 0;
                    for (int k = 0; k < div; k++)
                    {
                        for (int m = 0; m < div; m++)
                        {
                            sum = sum + inImage[i * div + k, j * div + m];
                        }
                    }
                    outImage[i, j] = (byte)(sum / (double)(div * div));
                }
            }
            displayImage();
        }

        void rotateImage()
        {//회전 알고리즘
            if (inImage == null)
            {
                return;
            }
            int degree = (int)getValue();
            outH = inH;
            outW = inW;
            outImage = new byte[outH, outW];
            calculRotate(degree);//회전 계산
            displayImage();
        }


        void calculRotate(int degree)
        {//회전 계산 알고리즘
            int center_w = inW / 2;//중심축 계산
            int center_h = inH / 2;
            int new_w;
            int new_h;
            double pi = 3.141592;
            // -degree 반시계 방향 회전
            // degree 시계 방향 회전
            double seta = pi / (180.0 / degree);
            //회전 알고리즘
            for (int i = 0; i < inH; i++)
            {
                for (int j = 0; j < inW; j++)
                {
                    new_w = (int)((i - center_h) * Math.Sin(seta) + (j - center_w) * Math.Cos(seta) + center_w);
                    new_h = (int)((i - center_h) * Math.Cos(seta) - (j - center_w) * Math.Sin(seta) + center_h);
                    if (new_w < 0) continue;
                    if (new_w >= inW) continue;
                    if (new_h < 0) continue;
                    if (new_h >= inH) continue;
                    outImage[i, j] = inImage[new_h, new_w];
                }
            }
            //회전 보간법 알고리즘 (hole 채우기)
            int left_pixval = 0;
            int right_pixval = 0;
            for (int i = 0; i < outH; i++)
            {
                for (int j = 0; j < outW; j++)
                {
                    if (j == 0)
                    {
                        right_pixval = outImage[i, j + 1];
                        left_pixval = right_pixval;
                    }
                    else if (j == outW - 1)
                    {
                        left_pixval = outImage[i, j - 1];
                        right_pixval = left_pixval;
                    }
                    else
                    {
                        left_pixval = outImage[i, j - 1];
                        right_pixval = outImage[i, j + 1];
                    }
                    if (outImage[i, j] == 0 && left_pixval != 0 && right_pixval != 0)
                    {
                        outImage[i, j] = (byte)((left_pixval + right_pixval) / 2);
                    }
                }
            }
        }
        void fillEdges(double[,] tmpInput, double[,] inputCopy)
        {//가장자리 처리 알고리즘 5x5 마스크 전용
            for (int i = 0; i < inH + 4; i++)
            {
                for (int j = 0; j < inW + 4; j++)
                {
                    if (i < 2 && j < 2)//왼쪽 위 모서리
                    {
                        tmpInput[i, j] = inputCopy[i, j];
                    }
                    else if (i < 2 && j > inW + 1) //오른쪽 위 모서리
                    {
                        tmpInput[i, j] = inputCopy[i, j - 4];
                    }
                    else if (i < 2)//맨 위 2줄
                    {
                        tmpInput[i, j] = inputCopy[i, j - 2];
                    }
                    else if (i > inH + 1 && j < 2)//왼쪽 아래 모서리
                    {
                        tmpInput[i, j] = inputCopy[i - 4, j];
                    }
                    else if (i > inH + 1 && j > inW + 1)//오른쪽 아래 모서리
                    {
                        tmpInput[i, j] = inputCopy[i - 4, j - 4];
                    }
                    else if (j < 2)//맨 왼쪽 2줄
                    {
                        tmpInput[i, j] = inputCopy[i - 2, j];
                    }
                    else if (j > inW + 1)//맨 오른쪽 2줄
                    {
                        tmpInput[i, j] = inputCopy[i - 2, j - 4];
                    }
                    else if (i > inH + 1)//맨 아래 2줄
                    {
                        tmpInput[i, j] = inputCopy[i - 4, j - 2];
                    }
                    else
                    {
                        tmpInput[i, j] = inputCopy[i - 2, j - 2];
                    }
                }
            }
        }
        //단순 마스크 처리
        void maskOP(double[,] tmpInput, double[,] tmpOutput, double[,] mask)
        {
            double sum = 0.0;
            for (int i = 2; i < inH + 2; i++)
            {
                for (int j = 2; j < inW + 2; j++)
                {
                    for (int k = 0; k < 5; k++)
                    {
                        for (int m = 0; m < 5; m++)
                        {
                            sum += tmpInput[i - 2 + k, j - 2 + m] * mask[k, m];
                        }
                    }
                    tmpOutput[i - 2, j - 2] = sum;
                    sum = 0.0;
                }
            }
        }
        void outCopy(double[,] tmpOutput)
        { //임시 출력 -> 원래 출력
            for (int i = 0; i < inH; i++)
            {
                for (int j = 0; j < inW; j++)
                {
                    if (tmpOutput[i, j] > 255.0)
                    {
                        outImage[i, j] = (byte)255;
                    }
                    else if (tmpOutput[i, j] < 0.0)
                    {
                        outImage[i, j] = (byte)0;
                    }
                    else
                    {
                        outImage[i, j] = (byte)tmpOutput[i, j];
                    }
                }
            }
        }
        void embossImage()
        {
            if (inImage == null)
            {
                return;
            }
            outH = inH;
            outW = inW;
            outImage = new byte[outH, outW];
            //마스크 결정
            double[,] mask = { 
                { -1.0, -1.0, 0.0, 0.0, 0.0}, 
                { -1.0, -1.0, 0.0, 0.0, 0.0}, 
                { 0.0, 0.0, 0.0, 0.0, 0.0}, 
                { 0.0, 0.0, 0.0, 1.0, 1.0}, 
                { 0.0, 0.0, 0.0, 1.0, 1.0} };
            //임시 입력 출력 메모리 할당
            double[,] tmpInput = new double[inH + 4, inW + 4];//확장된 메모리
            double[,] inputCopy = new double[inH, inW];//가장자리 처리를 위한 복사본 메모리
            double[,] tmpOutput = new double[inH, inW];//후처리를 위한 출력 메모리
            //입력 -> 복사본 복사
            for(int i = 0; i < inH; i++)
            {
                for(int j = 0; j < inW; j++)
                {
                    inputCopy[i, j] = inImage[i, j];
                }
            }
            //가장자리 처리
            fillEdges(tmpInput, inputCopy);
            //마스크연산
            maskOP(tmpInput, tmpOutput, mask);
            //후처리 Mask의 합이 0이면 127 더하기
            for (int i = 0; i < inH; i++)
            {
                for (int j = 0; j < inW; j++)
                {
                    tmpOutput[i, j] += 127;
                }
            }
            //임시 출력 -> 원래 출력
            outCopy(tmpOutput);
            displayImage();
        }

        void blurrImage()
        {
            if (inImage == null)
            {
                return;
            }
            outH = inH;
            outW = inW;
            outImage = new byte[outH, outW];
            //화소 영역 처리
            //마스크 결정
            double[,] mask = {
                { 1.0/25.0, 1.0/25.0, 1.0/25.0, 1.0/25.0, 1.0/25.0},
                { 1.0/25.0, 1.0/25.0, 1.0/25.0, 1.0/25.0, 1.0/25.0},
                { 1.0/25.0, 1.0/25.0, 1.0/25.0, 1.0/25.0, 1.0/25.0},
                { 1.0/25.0, 1.0/25.0, 1.0/25.0, 1.0/25.0, 1.0/25.0},
                { 1.0/25.0, 1.0/25.0, 1.0/25.0, 1.0/25.0, 1.0/25.0} };
            //임시 입력 출력 메모리 할당
            double[,] tmpInput = new double[inH + 4, inW + 4];//확장된 메모리
            double[,] inputCopy = new double[inH, inW];//가장자리 처리를 위한 복사본 메모리
            double[,] tmpOutput = new double[inH, inW];//후처리를 위한 출력 메모리
            //입력 -> 복사본 복사
            for (int i = 0; i < inH; i++)
            {
                for (int j = 0; j < inW; j++)
                {
                    inputCopy[i, j] = inImage[i, j];
                }
            }
            //가장자리 처리
            fillEdges(tmpInput, inputCopy);
            //마스크 연산
            maskOP(tmpInput, tmpOutput, mask);
            //임시 출력 -> 원래 출력
            outCopy(tmpOutput);
            displayImage();
        }

        void sharpImage()
        {
            if (inImage == null)
            {
                return;
            }
            outH = inH;
            outW = inW;
            outImage = new byte[outH, outW];
            //화소 영역 처리
            //마스크 결정
            double[,] mask = {
                { -1, -1, -1, -1, -1},
                { -1, -1, -1, -1, -1},
                { -1, -1, 24, -1, -1},
                { -1, -1, -1, -1, -1},
                { -1, -1, -1, -1, -1} };
            //임시 입력 출력 메모리 할당
            double[,] tmpInput = new double[inH + 4, inW + 4];//확장된 메모리
            double[,] inputCopy = new double[inH, inW];//가장자리 처리를 위한 복사본 메모리
            double[,] tmpOutput = new double[inH, inW];//후처리를 위한 출력 메모리
            //입력 -> 복사본 복사
            for (int i = 0; i < inH; i++)
            {
                for (int j = 0; j < inW; j++)
                {
                    inputCopy[i, j] = inImage[i, j];
                }
            }
            //가장자리 처리
            fillEdges(tmpInput, inputCopy);
            //마스크 연산
            maskOP(tmpInput, tmpOutput, mask);
            //임시 출력 -> 원래 출력
            outCopy(tmpOutput);
            displayImage();
        }

        //모폴로지 연산
        void morphologyOP(double[,] tmpInput, double[,] tmpOutput, int isErosion)
        { //침식(isErosion = 1) min 팽창(isErosion = 0) max 
            double min, max;
            for (int i = 2; i < inH + 2; i++)
            {
                for (int j = 2; j < inW + 2; j++)
                {
                    min = 255;
                    max = 0;
                    for (int k = 0; k < 5; k++)
                    {
                        for (int m = 0; m < 5; m++)
                        {
                            if(isErosion == 1)//침식
                            {
                                if (tmpInput[i - 2 + k, j - 2 + m] < min)
                                {
                                    min = tmpInput[i - 2 + k, j - 2 + m];
                                }
                            } else//팽창
                            {
                                if (tmpInput[i - 2 + k, j - 2 + m] > max)
                                {
                                    max = tmpInput[i - 2 + k, j - 2 + m];
                                }
                            }
                        }
                    }
                    if (isErosion == 1)//침식
                    {
                        tmpOutput[i - 2, j - 2] = min;
                    } else
                    {
                        tmpOutput[i - 2, j - 2] = max;
                    }
                }
            }
        }
        void erosionGray()
        {
            if (inImage == null)
            {
                return;
            }
            outH = inH;
            outW = inW;
            outImage = new byte[outH, outW];
            double[,] tmpInput = new double[inH + 4, inW + 4];//확장된 메모리
            double[,] inputCopy = new double[inH, inW];//가장자리 처리를 위한 복사본 메모리
            double[,] tmpOutput = new double[inH, inW];//후처리를 위한 출력 메모리
            for (int i = 0; i < inH; i++)
            {
                for (int j = 0; j < inW; j++)
                {
                    inputCopy[i, j] = inImage[i, j];
                }
            }
            //가장자리 처리
            fillEdges(tmpInput, inputCopy);
            morphologyOP(tmpInput, tmpOutput, 1);
            //임시 출력 -> 원래 출력
            outCopy(tmpOutput);
            displayImage();
        }
        
        void dilationGray()
        {
            if (inImage == null)
            {
                return;
            }
            outH = inH;
            outW = inW;
            outImage = new byte[outH, outW];
            double[,] tmpInput = new double[inH + 4, inW + 4];//확장된 메모리
            double[,] inputCopy = new double[inH, inW];//가장자리 처리를 위한 복사본 메모리
            double[,] tmpOutput = new double[inH, inW];//후처리를 위한 출력 메모리
            for (int i = 0; i < inH; i++)
            {
                for (int j = 0; j < inW; j++)
                {
                    inputCopy[i, j] = inImage[i, j];
                }
            }
            //가장자리 처리
            fillEdges(tmpInput, inputCopy);
            morphologyOP(tmpInput, tmpOutput, 0);
            //임시 출력 -> 원래 출력
            outCopy(tmpOutput);
            displayImage();
        }
        void openingGray()
        {
            if (inImage == null)
            {
                return;
            }
            outH = inH;
            outW = inW;
            outImage = new byte[outH, outW];
            double[,] tmpInput = new double[inH + 4, inW + 4];//확장된 메모리
            double[,] inputCopy = new double[inH, inW];//가장자리 처리를 위한 복사본 메모리
            double[,] tmpOutput = new double[inH, inW];//후처리를 위한 출력 메모리
            for (int i = 0; i < inH; i++)
            {
                for (int j = 0; j < inW; j++)
                {
                    inputCopy[i, j] = inImage[i, j];
                }
            }
            //가장자리 처리
            fillEdges(tmpInput, inputCopy);
            morphologyOP(tmpInput, tmpOutput, 1);
            //2번 가장차리 처리 연산을 위한 메모리 
            double[,] outBufImage = new double[outH + 4, outW + 4]; 
            //가장자리 처리
            fillEdges(outBufImage, tmpOutput);
            morphologyOP(outBufImage, tmpOutput, 0);
            //임시 출력 -> 원래 출력
            outCopy(tmpOutput);
            displayImage();
        }

        void closingGray()
        {
            if (inImage == null)
            {
                return;
            }
            outH = inH;
            outW = inW;
            outImage = new byte[outH, outW];
            double[,] tmpInput = new double[inH + 4, inW + 4];//확장된 메모리
            double[,] inputCopy = new double[inH, inW];//가장자리 처리를 위한 복사본 메모리
            double[,] tmpOutput = new double[inH, inW];//후처리를 위한 출력 메모리
            for (int i = 0; i < inH; i++)
            {
                for (int j = 0; j < inW; j++)
                {
                    inputCopy[i, j] = inImage[i, j];
                }
            }
            //가장자리 처리
            fillEdges(tmpInput, inputCopy);
            morphologyOP(tmpInput, tmpOutput, 0);
            //2번 가장차리 처리 연산을 위한 메모리 
            double[,] outBufImage = new double[outH + 4, outW + 4];
            //가장자리 처리
            fillEdges(outBufImage, tmpOutput);
            morphologyOP(outBufImage, tmpOutput, 1);
            //임시 출력 -> 원래 출력
            outCopy(tmpOutput);
            displayImage();
        }
        //모폴로지 이진화 이미지 (가장자리 처리하지 않음, 단순처리)
        void erosionBW()
        {
            if (inImage == null)
            {
                return;
            }
            outH = inH;
            outW = inW;
            outImage = new byte[outH, outW];
            calculErosionBW();
            displayImage();
        }

        void calculErosionBW()
        {
            int[,] erosionMask = { //마스크 설정
                    { 0, 0, 1, 0, 0},
                    { 1, 1, 1, 1, 1},
                    { 1, 1, 1, 1, 1},
                    { 1, 1, 1, 1, 1},
                    { 0, 0, 1, 0, 0} };
            byte isErosion;
            for (int i = 2; i < inH - 2; i++)
            {//가장자리는 처리하지 않음
                for (int j = 2; j < inW - 2; j++)
                {
                    isErosion = 255;
                    for (int k = 0; k < 5; k++)
                    {
                        for (int m = 0; m < 5; m++)
                        {
                            if (erosionMask[k, m] == 1)
                            {
                                if (inImage[i - 2 + k, j - 2 + m] == 0)
                                {//이미지가 0이라면
                                    isErosion = 0;//침식
                                    outImage[i, j] = isErosion;
                                    break;
                                }
                            }
                        }
                        if (isErosion == 0)
                        {
                            break;
                        }
                    }
                    if (isErosion == 255)
                    {//마스크를 모두 통과했다면
                        outImage[i, j] = isErosion;
                    }
                }
            }
        }

        void dilationBW()
        {
            if (inImage == null)
            {
                return;
            }
            outH = inH;
            outW = inW;
            outImage = new byte[outH, outW];
            //화소 영역 처리
            //마스크 결정
            calculDilationBW();
            displayImage();
        }
        void calculDilationBW()
        {
            int[,] dilationMask = { //마스크 설정
                    { 0, 0, 1, 0, 0},
                    { 1, 1, 1, 1, 1},
                    { 1, 1, 1, 1, 1},
                    { 1, 1, 1, 1, 1},
                    { 0, 0, 1, 0, 0} };
            byte isDilation;
            for (int i = 2; i < inH - 2; i++)
            {//가장자리는 처리하지 않음
                for (int j = 2; j < inW - 2; j++)
                {
                    isDilation = 0;
                    for (int k = 0; k < 5; k++)
                    {
                        for (int m = 0; m < 5; m++)
                        {
                            if (dilationMask[k, m] == 1)
                            {
                                if (inImage[i - 2 + k, j - 2 + m] == 255)
                                {//이미지가 255라면
                                    isDilation = 255;//팽창
                                    outImage[i, j] = isDilation;
                                    break;
                                }
                            }
                        }
                        if (isDilation == 255)
                        {
                            break;
                        }
                    }
                    if (isDilation == 0)
                    {//마스크를 모두 통과했다면
                        outImage[i, j] = isDilation;
                    }
                }
            }
        }

        void openingBW()
        {
            if (inImage == null)
            {
                return;
            }
            outH = inH;
            outW = inW;
            outImage = new byte[outH, outW];
            //화소 영역 처리
            //마스크 결정
            calculErosionBW();
            //임시 출력 메모리 생성
            byte[,] outBufImage = new byte[outH, outW];
            int[,] dilationMask = { //마스크 설정
                { 0, 1, 0},
                { 1, 1, 1},
                { 0, 1, 0} };
            byte isDilation;
            for (int i = 1; i < inH - 1; i++)
            {//엣지는 처리하지 않음
                for (int j = 1; j < inW - 1; j++)
                {
                    isDilation = 0;
                    for (int k = 0; k < 3; k++)
                    {
                        for (int m = 0; m < 3; m++)
                        {
                            if (dilationMask[k, m] == 1)
                            {//mask 값이 1이고
                                if (outImage[i - 1 + k, j - 1 + m] == 255)
                                {//이미지가 255라면
                                    isDilation = 255;//팽창
                                    outBufImage[i, j] = isDilation;
                                    break;
                                }
                            }
                        }
                        if (isDilation == 255)
                        {
                            break;
                        }
                    }
                    if (isDilation == 0)
                    {//마스크를 모두 통과했다면
                        outBufImage[i, j] = isDilation;
                    }
                }
            }
            //임시 출력 -> 출력
            for (int i = 0; i < outH; i++)
            {
                for (int j = 0; j < outW; j++)
                {
                    outImage[i, j] = outBufImage[i, j];
                }
            }
            displayImage();
        }

        void closingBW()
        {
            if (inImage == null)
            {
                return;
            }
            outH = inH;
            outW = inW;
            outImage = new byte[outH, outW];
            //화소 영역 처리
            //마스크 결정
            calculDilationBW();
            //임시 출력 메모리 생성
            byte[,] outBufImage = new byte[outH, outW];
            int[,] erosionMask = { //마스크 설정
                { 0, 1, 0},
                { 1, 1, 1},
                { 0, 1, 0} };
            byte isErosion;
            for (int i = 1; i < inH - 1; i++)
            {//가장자리는 처리하지 않음
                for (int j = 1; j < inW - 1; j++)
                {
                    isErosion = 255;
                    for (int k = 0; k < 3; k++)
                    {
                        for (int m = 0; m < 3; m++)
                        {
                            if (erosionMask[k, m] == 1)
                            {
                                if (outImage[i - 1 + k, j - 1 + m] == 0)
                                {//이미지가 0이라면
                                    isErosion = 0;//침식
                                    outBufImage[i, j] = isErosion;
                                    break;
                                }
                            }
                        }
                        if (isErosion == 0)
                        {
                            break;
                        }
                    }
                    if (isErosion == 255)
                    {//마스크를 모두 통과했다면
                        outBufImage[i, j] = isErosion;
                    }
                }
            }
            //임시 출력 -> 출력
            for (int i = 0; i < outH; i++)
            {
                for (int j = 0; j < outW; j++)
                {
                    outImage[i, j] = outBufImage[i, j];
                }
            }
            displayImage();
        }

        void medianFilter()
        {//노이즈 제거 알고리즘 노이즈 이미지 저장기능 구현하지 않음
            if (inImage == null)
            {
                return;
            }
            outH = inH;
            outW = inW;
            outImage = new byte[outH, outW];
            double[,] tmpInput = new double[inH + 4, inW + 4];//확장된 메모리
            double[,] tmpOutput = new double[inH, inW];//가장자리 처리를 위한 출력 메모리
            double[,] inputCopy = new double[inH, inW];//노이즈 추가 이미지
            for (int i = 0; i < inH; i++)
            {
                for (int j = 0; j < inW; j++)
                {
                    inputCopy[i, j] = inImage[i, j];
                }
            }
            int amount = 10;//잡음 개수 조절
                            //가로 x 세로 x (amout / 100)
            int noiseCount = (int)(inH * inW * ((double)amount / 100));
            salt_pepper(noiseCount, inputCopy);//영상에 잡음추가
            //잡음 이미지 출력
            for (int i = 0; i < inH; i++)
            {
                for (int j = 0; j < inW; j++)
                {
                    outImage[i, j] = (byte)inputCopy[i, j];
                }
            }
            displayImage();
            Delay(3000);
            fillEdges(tmpInput, inputCopy);
            int sortSize = 5;
            byte[] medianSort = new byte[sortSize * sortSize];//정렬을 위한 1차원 배열
            //inImage 값이 아닌 노이즈가 생긴 tmpInput 값을 가져온다.
            int temp = 0;//임시변수(배열 인덱스 값)
            for (int i = 2; i < inH + 2; i++)
            {//엣지는 처리하지 않음
                for (int j = 2; j < inW + 2; j++)
                {
                    temp = 0;
                    for (int k = 0; k < sortSize; k++)
                    {
                        for (int m = 0; m < sortSize; m++)
                        {
                            medianSort[temp++] = (byte)tmpInput[i - 2 + k, j - 2 + m];
                        }
                    }
                    Array.Sort(medianSort);
                    outImage[i - 2, j - 2] = medianSort[(sortSize * sortSize) / 2];//중간 값으로 출력
                }
            }
            //제거 이미지 출력
            displayImage();
        }

        //프로그램 ms만큼 대기
        private DateTime Delay(int MS)
        {
            DateTime ThisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
            DateTime AfterWards = ThisMoment.Add(duration);

            while (AfterWards >= ThisMoment)
            {
                System.Windows.Forms.Application.DoEvents();
                ThisMoment = DateTime.Now;
            }

            return DateTime.Now;
        }

        void salt_pepper(int noiseCount, double[,] inputCopy)
        {//영상에 잡음 추가
            Random r = new Random();
            int salt_or_pepper;
            int row, col;
            //잡음 추가
            for (int i = 0; i < noiseCount; i++)
            {
                row = r.Next(0, inH);
                col = r.Next(0, inW);
                // 랜덤하게 0 또는 255, 0이면 후추, 255면 소금
                salt_or_pepper = r.Next(0, 2) * 255;
                inputCopy[row, col] = salt_or_pepper;
            }
        }

        void histoImage()
        {
            if (inImage == null)
                return;
            outH = inH;
            outW = inW;
            outImage = new byte[outH, outW];
            byte min = inImage[0, 0];
            byte max = inImage[0, 0];
            for (int i = 0; i < inH; i++)
            {
                for (int j = 0; j < inW; j++)
                {
                    if (min > inImage[i, j])
                    {
                        min = inImage[i, j];
                    }
                    if (max < inImage[i, j])
                    {
                        max = inImage[i, j];
                    }
                }
            }

            for (int i = 0; i < inH; i++)
            {
                for (int j = 0; j < inW; j++)
                {
                    outImage[i, j] = (byte)(((double)(inImage[i, j] - min) / (max - min)) * 255);
                }
            }
            displayImage();
        }

        void endInSearch()
        {
            if (inImage == null)
                return;
            outH = inH;
            outW = inW;
            outImage = new byte[outH, outW];
            byte min = inImage[0, 0];
            byte max = inImage[0, 0];
            for (int i = 0; i < inH; i++)
            {
                for (int j = 0; j < inW; j++)
                {
                    if (min > inImage[i, j])
                    {
                        min = inImage[i, j];
                    }
                    if (max < inImage[i, j])
                    {
                        max = inImage[i, j];
                    }
                }
            }
            //min max값을 강제로 변경
            min += 50;
            max -= 50;
            for (int i = 0; i < inH; i++)
            {
                for (int j = 0; j < inW; j++)
                {
                    double value = ((double)(inImage[i, j] - min) / (max - min)) * 255;
                    if (value > 255)
                    {
                        value = 255;
                    }
                    else if (value < 0)
                    {
                        value = 0;
                    }
                    outImage[i, j] = (byte)value;
                }
            }
            displayImage();
        }

        void histEqualized()
        {
            if (inImage == null)
                return;
            outH = inH;
            outW = inW;
            outImage = new byte[outH, outW];
            //히스토그램 생성
            int[] hist = new int[256];
            int[] histSum = new int[256];
            for (int i = 0; i < hist.Length; i++)
            {
                hist[i] = 0;
                histSum[i] = 0;
            }
            for (int i = 0; i < inH; i++)
            {
                for (int j = 0; j < inW; j++)
                {
                    hist[inImage[i, j]]++;
                }
            }
            //누적합 생성
            int copy = hist[0];
            histSum[0] = copy;
            for (int i = 1; i < histSum.Length; i++)
            {
                histSum[i] = (histSum[i - 1] + hist[i]);
            }
            //정규화된 누적합 생성
            for (int i = 0; i < inH; i++)
            {
                for (int j = 0; j < inW; j++)
                {
                    outImage[i, j] = (byte)(histSum[inImage[i, j]] / (double)(inH * inW) * 255);
                }
            }
            displayImage();
        }
    }
}
