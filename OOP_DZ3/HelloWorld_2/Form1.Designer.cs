namespace HelloWorld_2;

partial class Form1
{
    private System.ComponentModel.IContainer? components = null;
    private System.Windows.Forms.Label label1 = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.label1 = new System.Windows.Forms.Label();
        this.SuspendLayout();

        this.label1.AutoSize = true;
        this.label1.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.label1.Location = new System.Drawing.Point(250, 180);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(230, 50);
        this.label1.TabIndex = 0;
        this.label1.Text = "Hello World!";

        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Controls.Add(this.label1);
        this.Name = "Form1";
        this.Text = "Form1";
        this.ResumeLayout(false);
        this.PerformLayout();
    }
}
