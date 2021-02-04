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

        private void Form1_Load(object sender, EventArgs e)
        {

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
            erosionImage();
        }

        private void dilationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dilationImage();
        }

        private void openingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openingImage();
        }

        private void closingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closingImage();
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
            for (int i = 0; i < inH; i++)
            {
                for (int j = 0; j < inW; j++)
                {
                    outImage[i, j] = (byte)(255 - inImage[i, j]);
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

        void embossImage()
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
            const int MSIZE = 3;
            double[,] mask = { 
                { -1.0, 0.0, 0.0}, 
                { 0.0, 0.0, 0.0}, 
                { 0.0, 0.0, 1.0} };
            //임시 입력 출력 메모리 할당
            double[,] tmpInput = new double[inH + 2, inW + 2];
            double[,] tmpOutput = new double[outH, outW];

            //임시 입력을 중간값인 127로 초기화 -> 평균 값으로 하거나 가장자리 값을 땡겨온다
            for(int i = 0; i < inH + 2; i++)
            {
                for(int j = 0; j < inW + 2; j++)
                {
                    tmpInput[i, j] = 127;
                }
            }
            //입력 -> 임시 입력
            for (int i = 0; i < inH; i++)
            {
                for (int j = 0; j < inW; j++)
                {
                    tmpInput[i + 1, j + 1] = inImage[i,j];
                }
            }
            double sum = 0.0;
            for (int i = 0; i < inH; i++)
            {
                for (int j = 0; j < inW; j++)
                {
                    for(int k = 0; k < MSIZE; k++)
                    {
                        for(int m = 0; m < MSIZE; m++) 
                        {
                            sum += tmpInput[i + k, j + m] * mask[k, m];
                        }
                    }
                    tmpOutput[i, j] = sum;
                    sum = 0.0;
                }
            }

            //후처리 Mask의 합이 0이면 127 더하기
            for (int i = 0; i < inH; i++)
            {
                for (int j = 0; j < inW; j++)
                {
                    tmpOutput[i, j] += 127;
                }
            }
            //임시 출력 -> 원래 출력
            for (int i = 0; i < inH; i++)
            {
                for (int j = 0; j < inW; j++)
                {
                    double d = tmpOutput[i, j];
                    if(d > 255.0)
                    {
                        d = 255.0;
                    } else if (d < 0.0)
                    {
                        d = 00;
                    } else
                    {
                        outImage[i, j] = (byte)d;
                    }
                }
            }
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
            const int MSIZE = 3;
            double[,] mask = {
                { 1/9.0, 1/9.0, 1/9.0},
                { 1/9.0, 1/9.0, 1/9.0},
                { 1/9.0, 1/9.0, 1/9.0} };
            //임시 입력 출력 메모리 할당
            double[,] tmpInput = new double[inH + 2, inW + 2];
            double[,] tmpOutput = new double[outH, outW];

            //임시 입력을 중간값인 127로 초기화 -> 평균 값으로 하거나 가장자리 값을 땡겨온다
            for (int i = 0; i < inH + 2; i++)
            {
                for (int j = 0; j < inW + 2; j++)
                {
                    tmpInput[i, j] = 127;
                }
            }
            //입력 -> 임시 입력
            for (int i = 0; i < inH; i++)
            {
                for (int j = 0; j < inW; j++)
                {
                    tmpInput[i + 1, j + 1] = inImage[i, j];
                }
            }
            double sum = 0.0;
            for (int i = 0; i < inH; i++)
            {
                for (int j = 0; j < inW; j++)
                {
                    for (int k = 0; k < MSIZE; k++)
                    {
                        for (int m = 0; m < MSIZE; m++)
                        {
                            sum += tmpInput[i + k, j + m] * mask[k, m];
                        }
                    }
                    tmpOutput[i, j] = sum;
                    sum = 0.0;
                }
            }

            //후처리 Mask의 합이 0이면 127 더하기
           /* for (int i = 0; i < inH; i++)
            {
                for (int j = 0; j < inW; j++)
                {
                    tmpOutput[i, j] += 127;
                }
            }*/
            //임시 출력 -> 원래 출력
            for (int i = 0; i < inH; i++)
            {
                for (int j = 0; j < inW; j++)
                {
                    double d = tmpOutput[i, j];
                    if (d > 255.0)
                    {
                        d = 255.0;
                    }
                    else if (d < 0.0)
                    {
                        d = 00;
                    }
                    else
                    {
                        outImage[i, j] = (byte)d;
                    }
                }
            }
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
            const int MSIZE = 3;
            double[,] mask = {
                { -1, -1, -1},
                { -1, 9, -1},
                { -1, -1, -1} };
            //임시 입력 출력 메모리 할당
            double[,] tmpInput = new double[inH + 2, inW + 2];
            double[,] tmpOutput = new double[outH, outW];

            //임시 입력을 중간값인 127로 초기화 -> 평균 값으로 하거나 가장자리 값을 땡겨온다
            for (int i = 0; i < inH + 2; i++)
            {
                for (int j = 0; j < inW + 2; j++)
                {
                    tmpInput[i, j] = 127;
                }
            }
            //입력 -> 임시 입력
            for (int i = 0; i < inH; i++)
            {
                for (int j = 0; j < inW; j++)
                {
                    tmpInput[i + 1, j + 1] = inImage[i, j];
                }
            }
            double sum = 0.0;
            for (int i = 0; i < inH; i++)
            {
                for (int j = 0; j < inW; j++)
                {
                    for (int k = 0; k < MSIZE; k++)
                    {
                        for (int m = 0; m < MSIZE; m++)
                        {
                            sum += tmpInput[i + k, j + m] * mask[k, m];
                        }
                    }
                    tmpOutput[i, j] = sum;
                    sum = 0.0;
                }
            }
            for (int i = 0; i < inH; i++)
            {
                for (int j = 0; j < inW; j++)
                {
                    double d = tmpOutput[i, j];
                    if (d > 255.0)
                    {
                        d = 255.0;
                    }
                    else if (d < 0.0)
                    {
                        d = 00;
                    }
                    else
                    {
                        outImage[i, j] = (byte)d;
                    }
                }
            }
            displayImage();
        }

        void erosionImage()
        {
            if (inImage == null)
            {
                return;
            }
            outH = inH;
            outW = inW;
            outImage = new byte[outH, outW];
            byte[,] tmpInput = new byte[inH + 4, inW + 4];//확장된 메모리
            byte[,] inputCopy = new byte[inH, inW];//가장자리 처리를 위한 복사본 메모리
            byte[,] tmpOutput = new byte[inH, inW];//가장자리 처리를 위한 출력 메모리
            inputCopy = (byte[,])inImage.Clone();
            calculErosion(tmpInput, inputCopy, tmpOutput);
            //후처리
            outImage = (byte[,])tmpOutput.Clone();
            displayImage();
        }
        
        void fillEdges(byte[,] tmpInput, byte[,] inputCopy)
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
        void calculErosion(byte[,] tmpInput, byte[,] inputCopy, byte[,] tmpOutput)
        {
            //가장자리 처리
            fillEdges(tmpInput, inputCopy);
            byte min;
            for (int i = 2; i < inH + 2; i++)
            {
                for (int j = 2; j < inW + 2; j++)
                {
                    min = 255;
                    for (int k = 0; k < 5; k++)
                    {
                        for (int m = 0; m < 5; m++)
                        {
                            if (tmpInput[i - 2 + k, j - 2 + m] < min)
                            {
                                min = tmpInput[i - 2 + k, j - 2 + m];
                            }
                        }
                    }
                    tmpOutput[i - 2, j - 2] = min;
                }
            }
        }

        void dilationImage()
        {
            if (inImage == null)
            {
                return;
            }
            outH = inH;
            outW = inW;
            outImage = new byte[outH, outW];
            byte[,] tmpInput = new byte[inH + 4, inW + 4];//확장된 메모리
            byte[,] inputCopy = new byte[inH, inW];//가장자리 처리를 위한 복사본 메모리
            byte[,] tmpOutput = new byte[inH, inW];//가장자리 처리를 위한 출력 메모리
            inputCopy = (byte[,])inImage.Clone();
            calculDilation(tmpInput, inputCopy, tmpOutput);
            //후처리
            outImage = (byte[,])tmpOutput.Clone();
            displayImage();
        }
        void calculDilation(byte[,] tmpInput, byte[,] inputCopy, byte[,] tmpOutput)
        {
            //가장자리 처리
            fillEdges(tmpInput, inputCopy);
            byte max;
            for (int i = 2; i < inH + 2; i++)
            {//가장자리는 처리하지 않음
                for (int j = 2; j < inW + 2; j++)
                {
                    max = 0;
                    for (int k = 0; k < 5; k++)
                    {
                        for (int m = 0; m < 5; m++)
                        {
                            if (tmpInput[i - 2 + k, j - 2 + m] > max)
                            {
                                max = (byte)tmpInput[i - 2 + k, j - 2 + m];
                            }
                        }
                    }
                    tmpOutput[i - 2,j - 2] = max;
                }
            }
        }

        void openingImage()
        {
            if (inImage == null)
            {
                return;
            }
            outH = inH;
            outW = inW;
            outImage = new byte[outH, outW];
            byte[,] tmpInput = new byte[inH + 4, inW + 4];//확장된 메모리
            byte[,] inputCopy = new byte[inH, inW];//가장자리 처리를 위한 복사본 메모리
            byte[,] tmpOutput = new byte[inH, inW];//가장자리 처리를 위한 출력 메모리
            inputCopy = (byte[,])inImage.Clone();
            //가장자리 처리
            fillEdges(tmpInput, inputCopy);
            calculErosion(tmpInput, inputCopy, tmpOutput);
            //2번 가장차리 처리 연산을 위한 메모리 
            byte[,] outBufImage = new byte[outH + 4, outW + 4]; 
            //가장자리 처리
            fillEdges(outBufImage, tmpOutput);
            byte max;
            for (int i = 2; i < inH + 2; i++)
            {
                for (int j = 2; j < inW + 2; j++)
                {
                    max = 0;
                    for (int k = 0; k < 5; k++)
                    {
                        for (int m = 0; m < 5; m++)
                        {
                            if (outBufImage[i - 2 + k, j - 2 + m] > max)
                            {
                                max = outBufImage[i - 2 + k, j - 2 + m];
                            }
                        }
                    }
                    outImage[i - 2, j - 2] = max;
                }
            }
            displayImage();
        }

        void closingImage()
        {
            if (inImage == null)
            {
                return;
            }
            outH = inH;
            outW = inW;
            outImage = new byte[outH, outW];
            byte[,] tmpInput = new byte[inH + 4, inW + 4];//확장된 메모리
            byte[,] inputCopy = new byte[inH, inW];//가장자리 처리를 위한 복사본 메모리
            byte[,] tmpOutput = new byte[inH, inW];//가장자리 처리를 위한 출력 메모리
            inputCopy = (byte[,])inImage.Clone();
            //가장자리 처리
            fillEdges(tmpInput, inputCopy);
            calculDilation(tmpInput, inputCopy, tmpOutput);
            //2번 가장차리 처리 연산을 위한 메모리 
            byte[,] outBufImage = new byte[outH + 4, outW + 4];
            //가장자리 처리
            fillEdges(outBufImage, tmpOutput);
            byte min;
            for (int i = 2; i < inH + 2; i++)
            {
                for (int j = 2; j < inW + 2; j++)
                {
                    min = 255;
                    for (int k = 0; k < 5; k++)
                    {
                        for (int m = 0; m < 5; m++)
                        {
                            if (outBufImage[i - 2 + k, j - 2 + m] < min)
                            {
                                min = outBufImage[i - 2 + k, j - 2 + m];
                            }
                        }
                    }
                    outImage[i - 2, j - 2] = min;
                }
            }
            displayImage();
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
