using System.Windows.Forms;

namespace MDI_Edtor
{
    public partial class Form1 : Form
    {
        private int formindex = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void customizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.ArrangeIcons);

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formindex = this.MdiChildren.Length;
            Form ChildForm = new Form(); //шинээр форм үүсгэх
            ChildForm.Name = "Child" + (formindex + 1).ToString(); //формын нэр зааж байна
            ChildForm.Text = "Child" + (formindex + 1).ToString(); //формын гарчиг зааж байна.
            ChildForm.MdiParent = this; //Эцэг цонхыг зааж байна.
            TextBox txtBox = new TextBox(); //текст бичих талбар үүсгэж байна
            txtBox.Dock = DockStyle.Fill; //форм дээр дүүргэж байна.
            txtBox.Multiline = true; // олон мөр текст бичих боломжтой болно.
            ChildForm.Controls.Add(txtBox); // хүү формын контролуудын массивт нэмж байна.
            ChildForm.Show(); // хүү формыг харагдуулж байна.
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);

        }

        private void tileHorizToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);

        }

        private void tileVertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form active = this.ActiveMdiChild; // одоо идэвхтэй байгаа child
            if (active == null)
            {
                MessageBox.Show("Хадгалах цонх алга байна.");
                return;
            }

            TextBox txtBox = active.Controls.OfType<TextBox>().FirstOrDefault();
            if (txtBox == null) return;

            string path = active.Tag as string; // өмнө нь хадгалсан/нээсэн файлын зам

            if (string.IsNullOrEmpty(path)) // анх удаа хадгалж байвал зам асууна
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Текст файл (*.txt)|*.txt|Бүх файл (*.*)|*.*";
                sfd.FileName = active.Text;
                if (sfd.ShowDialog() != DialogResult.OK) return;
                path = sfd.FileName;
            }

            System.IO.File.WriteAllText(path, txtBox.Text);
            active.Tag = path;                              // дараагийн удаа шууд хадгална
            active.Text = System.IO.Path.GetFileName(path); // гарчгийг файлын нэрээр солино
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form active = this.ActiveMdiChild; // одоо идэвхтэй байгаа child
            if (active == null)
            {
                MessageBox.Show("Хадгалах цонх алга байна.");
                return;
            }

            TextBox txtBox = active.Controls.OfType<TextBox>().FirstOrDefault();
            if (txtBox == null) return;

            string path = active.Tag as string; // өмнө нь хадгалсан/нээсэн файлын зам

            if (string.IsNullOrEmpty(path)) // анх удаа хадгалж байвал зам асууна
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Текст файл (*.txt)|*.txt|Бүх файл (*.*)|*.*";
                sfd.FileName = active.Text;
                if (sfd.ShowDialog() != DialogResult.OK) return;
                path = sfd.FileName;
            }

            System.IO.File.WriteAllText(path, txtBox.Text);
            active.Tag = path;                              // дараагийн удаа шууд хадгална
            active.Text = System.IO.Path.GetFileName(path); // гарчгийг файлын нэрээр солино
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form active = this.ActiveMdiChild; // одоо идэвхтэй байгаа child
            if (active == null)
            {
                MessageBox.Show("Буцаах үйлдэл хийх цонх алга байна.");
                return;
            }

            TextBox txtBox = active.Controls.OfType<TextBox>().FirstOrDefault();
            if (txtBox == null) return;

            if (txtBox.CanUndo)
                txtBox.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextBox txtBox = this.ActiveMdiChild.Controls.OfType<TextBox>().FirstOrDefault();
            if (txtBox == null) return;
            txtBox.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextBox txtBox = this.ActiveMdiChild.Controls.OfType<TextBox>().FirstOrDefault();
            if (txtBox == null) return;
            txtBox.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextBox txtBox = this.ActiveMdiChild.Controls.OfType<TextBox>().FirstOrDefault();
            if (txtBox == null) return;
            txtBox.Paste();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextBox txtBox = this.ActiveMdiChild.Controls.OfType<TextBox>().FirstOrDefault();
            if (txtBox == null) return;
            txtBox.SelectAll();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            openFileDialog.Title = "Файл нээх";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Идэвхтэй MDI цонхыг шалгах
                    Form child = this.ActiveMdiChild;

                    // Цонх байхгүй бол шинээр үүсгэх
                    if (child == null)
                    {
                        child = new Form();
                        child.MdiParent = this;
                    }

                    // TextBox хайх
                    TextBox txtBox = child.Controls
                        .OfType<TextBox>()
                        .FirstOrDefault();

                    // TextBox байхгүй бол шинээр үүсгэх
                    if (txtBox == null)
                    {
                        txtBox = new TextBox();
                        txtBox.Multiline = true;
                        txtBox.Dock = DockStyle.Fill;
                        txtBox.ScrollBars = ScrollBars.Both;

                        child.Controls.Add(txtBox);
                    }

                    // TXT файлын агуулгыг унших
                    txtBox.Text = System.IO.File.ReadAllText(
                        openFileDialog.FileName
                    );

                    // Цонхны гарчигт файлын нэрийг харуулах
                    child.Text = System.IO.Path.GetFileName(
                        openFileDialog.FileName
                    );

                    child.Show();
                    child.Activate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Алдаа: " + ex.Message);
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aboutBox about = new aboutBox();
            about.ShowDialog(this);
        }
    }

}
