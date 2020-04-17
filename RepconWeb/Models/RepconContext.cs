using Microsoft.EntityFrameworkCore;

namespace RepconWeb
{
    public partial class RepconContext : DbContext
    {
        public RepconContext(){}

        public RepconContext(DbContextOptions<RepconContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admn> Admn { get; set; }
        public virtual DbSet<Classroom> Classroom { get; set; }
        public virtual DbSet<Pc> Pc { get; set; }
        public virtual DbSet<Process> Process { get; set; }
        public virtual DbSet<Session> Session { get; set; }
        public virtual DbSet<SessionProc> SessionProc { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer("Server=localhost;Database=Repcon;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admn>(entity =>
            {
                entity.HasKey(e => e.Login)
                    .HasName("PK__Admn__7838F27330545BD1");

                entity.Property(e => e.Login)
                    .HasColumnName("login")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.LastSignIn)
                    .HasColumnName("last_sign_in")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.Pwd)
                    .HasColumnName("pwd")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.SignHash)
                    .HasColumnName("sign_hash")
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Classroom>(entity =>
            {
                entity.HasKey(e => e.CrId)
                    .HasName("PK__Classroo__AB69D8CF6FB9C292");

                entity.Property(e => e.CrId)
                    .HasColumnName("cr_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.ClassNum).HasColumnName("class_num");

                entity.Property(e => e.PlaceNum).HasColumnName("place_num");
            });

            modelBuilder.Entity<Pc>(entity =>
            {
                entity.ToTable("PC");

                entity.Property(e => e.PcId)
                    .HasColumnName("pc_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CrId).HasColumnName("cr_id");

                entity.Property(e => e.Macaddr)
                    .HasColumnName("macaddr")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.PcType)
                    .HasColumnName("pc_type")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.HasOne(d => d.Cr)
                    .WithMany(p => p.Pc)
                    .HasForeignKey(d => d.CrId)
                    .HasConstraintName("FK__PC__cr_id__412EB0B6");
            });

            modelBuilder.Entity<Process>(entity =>
            {
                entity.HasKey(e => e.ProcId)
                    .HasName("PK__Process__A21589458F2D9187");

                entity.Property(e => e.ProcId)
                    .HasColumnName("proc_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(15)
                    .IsUnicode(false); 

                entity.Property(e => e.Size).HasColumnName("size");
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.Property(e => e.SessionId)
                    .HasColumnName("session_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.EndT)
                    .HasColumnName("end_t")
                    .HasColumnType("datetime");

                entity.Property(e => e.Login)
                    .HasColumnName("login")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.PcId).HasColumnName("pc_id");

                entity.Property(e => e.StartT)
                    .HasColumnName("start_t")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Pc)
                    .WithMany(p => p.Session)
                    .HasForeignKey(d => d.PcId)
                    .HasConstraintName("FK__Session__pc_id__4222D4EF");
            });

            modelBuilder.Entity<SessionProc>(entity =>
            {
                entity.HasKey(e => e.SpId)
                    .HasName("PK__SessionP__4C367CEED0E4F87A");

                entity.Property(e => e.SpId)
                    .HasColumnName("sp_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.EndProc)
                    .HasColumnName("end_proc")
                    .HasColumnType("datetime");

                entity.Property(e => e.ProcId).HasColumnName("proc_id");

                entity.Property(e => e.SessionId).HasColumnName("session_id");

                entity.Property(e => e.StartProc)
                    .HasColumnName("start_proc")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Proc)
                    .WithMany(p => p.SessionProc)
                    .HasForeignKey(d => d.ProcId)
                    .HasConstraintName("FK__SessionPr__proc___4316F928");

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.SessionProc)
                    .HasForeignKey(d => d.SessionId)
                    .HasConstraintName("FK__SessionPr__sessi__440B1D61");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
