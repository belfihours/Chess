using System.Xml.Schema;

namespace Chess
{
    //VEDERE SE PUOI CAMBIARE IL STRING POS CON IL INT[] POS CHE è UN CASINO
    internal class Pieces
    {

        static public Pieces[,] table; // field
        public Pieces[,] Table   // property
        {
            get { return table; }
            set { table = value; }
        }

        static private List<int[]> possibleMoves; // field
        public List<int[]> PossibleMoves   // property
        {
            get { return possibleMoves; }
            set { possibleMoves = value; }
        }

        private int[] startPos = new int[2]; // field
        public int[] StartPos   // property
        {
            get { return startPos; }
            set { startPos = value; }
        }

        private int[] currentPos = new int[2]; // field
        public int[] CurrentPos   // property
        {
            get { return currentPos; }
            set { currentPos = value; }
        }

        private char letter = '\0'; // field
        public char Letter   // property
        {
            get { return letter; }
            set { letter = value; }
        }

        private char color; // field
        public char Color   // property
        {
            get { return color; }
            set { color = value; }
        }

        public bool Move(int[] currentpos, int[] nextpos, ref char color)
        {
            bool eat;
            if (IsEmpty(nextpos)) eat = false;
            else
            {
                eat = true;
                if (table[nextpos[0], nextpos[1]].GetType() == typeof(King))
                {
                    color = table[currentPos[0], currentPos[1]].color;
                }
            }
            if (eat && CastlingMove(currentpos, nextpos))
            {
                return false;
            }
            else
            {
                Eat(currentpos, nextpos);
            }

            return eat;
        }
        public virtual void Show()
        {

        }
        public class King : Pieces
        {

            public override void Show()
            {
                KingMoveShow();
            }

        }
        public class Queen : Pieces
        {
            public override void Show()
            {
                TowerMoveShow();
                BishopMoveShow();
            }

        }
        public class Tower : Pieces
        {
            public override void Show()
            {
                TowerMoveShow();
            }
        }
        public class Horse : Pieces
        {
            public override void Show()
            {
                HorseMoveShow();
            }
        }
        public class Bishop : Pieces
        {
            public override void Show()
            {
                BishopMoveShow();
            }
        }
        public class Pawn : Pieces
        {

            public override void Show()
            {
                PawnMoveShow();
            }
        }


        //---GENERAL METHODS
        private bool IsValid(int[] position)
        {
            bool valid = true;
            if (position == this.currentPos) valid = false;
            if (!IsInside(position)) valid = false;
            return valid;
        }
        private bool IsInside(int[] pos)
        {

            bool valid = true;
            for (int i = 0; i < pos.Length; i++)
            {
                if (pos[i] < 0 || pos[i] > 7)
                {
                    Console.WriteLine("Position outside table");
                    valid = false;
                }
            }

            return valid;
        }
        public bool IsEmpty(int[] pos)
        {
            if (table[pos[0], pos[1]] == null) return true;
            else return false;
        }
        public bool IsEmpty2(int j, int i)
        {
            if (table[j, i] == null) return true;
            else return false;
        }
        public void Eat(int[] currentpos, int[] nextpos)
        {
            table[nextpos[0], nextpos[1]] = table[currentpos[0], currentpos[1]];
            table[currentpos[0], currentpos[1]].currentPos = nextpos;
            table[currentpos[0], currentpos[1]] = null;

        }

        //---SPECIFIC METHODS

        public void PawnMoveShow()
        {
            int[] newPossiblePos = new int[2];
            int[] currentPosition = this.currentPos;

            if (currentPos.Equals(this.startPos))
            {

                if (this.color == 'b')
                {
                    newPossiblePos[0] = currentPosition[0];
                    newPossiblePos[1] = (currentPosition[1] + 1);
                    int[] toPass1 = { newPossiblePos[0], newPossiblePos[1] };
                    if (IsValid(toPass1) && IsEmpty(toPass1))
                    {
                        possibleMoves.Add(toPass1);
                        newPossiblePos[0] = currentPosition[0];
                        newPossiblePos[1] = (currentPosition[1] + 2);
                        int[] toPass2 = { newPossiblePos[0], newPossiblePos[1] };
                        if (IsValid(toPass1) && IsEmpty(toPass2)) possibleMoves.Add(toPass2);
                    }

                }
                else
                {
                    newPossiblePos[0] = currentPosition[0];
                    newPossiblePos[1] = (currentPosition[1] - 1);
                    int[] toPass1 = { newPossiblePos[0], newPossiblePos[1] };
                    if (IsValid(toPass1) && IsEmpty(toPass1))
                    {
                        possibleMoves.Add(toPass1);
                        newPossiblePos[0] = currentPosition[0];
                        newPossiblePos[1] = (currentPosition[1] - 2);
                        int[] toPass2 = { newPossiblePos[0], newPossiblePos[1] };
                        if (IsValid(toPass1) && IsEmpty(toPass2)) possibleMoves.Add(toPass2);
                    }

                }


            }
            else
            {
                if (this.color == 'b')
                {
                    newPossiblePos[0] = currentPosition[0];
                    newPossiblePos[1] = (currentPosition[1] + 1);
                    int[] toPass1 = { newPossiblePos[0], newPossiblePos[1] };
                    if (IsValid(toPass1) && IsEmpty(toPass1)) possibleMoves.Add(toPass1);
                }
                else
                {
                    newPossiblePos[0] = currentPosition[0];
                    newPossiblePos[1] = (currentPosition[1] - 1);
                    int[] toPass1 = { newPossiblePos[0], newPossiblePos[1] };
                    if (IsValid(toPass1) && IsEmpty(toPass1)) possibleMoves.Add(toPass1);
                }
            }
            if (this.color == 'b')
            {
                newPossiblePos[0] = currentPosition[0] - 1;
                newPossiblePos[1] = (currentPosition[1] + 1);
                int[] toPass1 = { newPossiblePos[0], newPossiblePos[1] };
                if (IsValid(toPass1) && !IsEmpty(toPass1) && table[toPass1[0], toPass1[1]].color != this.color) possibleMoves.Add(toPass1);
                newPossiblePos[0] = currentPosition[0] + 1;
                newPossiblePos[1] = (currentPosition[1] + 1);
                int[] toPass2 = { newPossiblePos[0], newPossiblePos[1] };
                if (IsValid(toPass2) && !IsEmpty(toPass2) && table[toPass2[0], toPass2[1]].color != this.color) possibleMoves.Add(toPass2);
            }
            else
            {
                newPossiblePos[0] = currentPosition[0] - 1;
                newPossiblePos[1] = (currentPosition[1] - 1);
                int[] toPass1 = { newPossiblePos[0], newPossiblePos[1] };
                if (IsValid(toPass1) && !IsEmpty(toPass1) && table[toPass1[0], toPass1[1]].color != this.color) possibleMoves.Add(toPass1);
                newPossiblePos[0] = currentPosition[0] + 1;
                newPossiblePos[1] = (currentPosition[1] - 1);
                int[] toPass2 = { newPossiblePos[0], newPossiblePos[1] };
                if (IsValid(toPass2) && !IsEmpty(toPass2) && table[toPass2[0], toPass2[1]].color != this.color) possibleMoves.Add(toPass2);
            }

        }

        public void BishopMoveShow()
        {
            int i;
            bool exit;
            int[] newPossiblePos;
            //DownDx
            i = 1;
            exit = false;
            while (!exit)
            {
                newPossiblePos = new int[2];
                newPossiblePos[0] = this.currentPos[0] + i;
                newPossiblePos[1] = this.currentPos[1] + i;
                if (IsValid(newPossiblePos))
                {
                    if (IsEmpty(newPossiblePos))
                    {
                        PossibleMoves.Add(newPossiblePos);
                    }
                    else
                    {
                        if (!table[newPossiblePos[0], newPossiblePos[1]].Color.Equals(this.color))
                        {
                            PossibleMoves.Add(newPossiblePos);
                        }
                        exit = true;
                    }
                }
                else exit = true;
                i++;
            }
            //DownSx
            i = 1;
            exit = false;
            while (!exit)
            {
                newPossiblePos = new int[2];
                newPossiblePos[0] = this.currentPos[0] - i;
                newPossiblePos[1] = this.currentPos[1] + i;
                if (IsValid(newPossiblePos))
                {
                    if (IsEmpty(newPossiblePos))
                    {
                        PossibleMoves.Add(newPossiblePos);
                    }
                    else
                    {
                        if (!table[newPossiblePos[0], newPossiblePos[1]].Color.Equals(this.color))
                        {
                            PossibleMoves.Add(newPossiblePos);
                        }
                        exit = true;
                    }
                }
                else exit = true;
                i++;
            }
            //UpSx
            i = 1;
            exit = false;
            while (!exit)
            {
                newPossiblePos = new int[2];
                newPossiblePos[0] = this.currentPos[0] - i;
                newPossiblePos[1] = this.currentPos[1] - i;
                if (IsValid(newPossiblePos))
                {
                    if (IsEmpty(newPossiblePos))
                    {
                        PossibleMoves.Add(newPossiblePos);
                    }
                    else
                    {
                        if (!table[newPossiblePos[0], newPossiblePos[1]].Color.Equals(this.color))
                        {
                            PossibleMoves.Add(newPossiblePos);
                        }
                        exit = true;
                    }
                }
                else exit = true;
                i++;
            }
            //UpDx
            i = 1;
            exit = false;
            while (!exit)
            {
                newPossiblePos = new int[2];
                newPossiblePos[0] = this.currentPos[0] + i;
                newPossiblePos[1] = this.currentPos[1] - i;
                if (IsValid(newPossiblePos))
                {
                    if (IsEmpty(newPossiblePos))
                    {
                        PossibleMoves.Add(newPossiblePos);
                    }
                    else
                    {
                        if (!table[newPossiblePos[0], newPossiblePos[1]].Color.Equals(this.color))
                        {
                            PossibleMoves.Add(newPossiblePos);
                        }
                        exit = true;
                    }
                }
                else exit = true;
                i++;
            }


        }

        public void TowerMoveShow()
        {
            int i;
            bool exit;
            int[] newPossiblePos;
            //Dx
            i = 1;
            exit = false;
            CastlingShowTower();
            while (!exit)
            {
                newPossiblePos = new int[2];
                newPossiblePos[0] = this.currentPos[0] + i;
                newPossiblePos[1] = this.currentPos[1];
                if (IsValid(newPossiblePos))
                {
                    if (IsEmpty(newPossiblePos))
                    {
                        PossibleMoves.Add(newPossiblePos);
                    }
                    else
                    {
                        if (!table[newPossiblePos[0], newPossiblePos[1]].Color.Equals(this.color))
                        {
                            PossibleMoves.Add(newPossiblePos);
                        }
                        exit = true;
                    }
                }
                else exit = true;
                i++;
            }
            //Sx
            i = 1;
            exit = false;
            while (!exit)
            {
                newPossiblePos = new int[2];
                newPossiblePos[0] = this.currentPos[0] - i;
                newPossiblePos[1] = this.currentPos[1];
                if (IsValid(newPossiblePos))
                {
                    if (IsEmpty(newPossiblePos))
                    {
                        PossibleMoves.Add(newPossiblePos);
                    }
                    else
                    {
                        if (!table[newPossiblePos[0], newPossiblePos[1]].Color.Equals(this.color))
                        {
                            PossibleMoves.Add(newPossiblePos);
                        }
                        exit = true;
                    }
                }
                else exit = true;
                i++;
            }
            //Up
            i = 1;
            exit = false;
            while (!exit)
            {
                newPossiblePos = new int[2];
                newPossiblePos[0] = this.currentPos[0];
                newPossiblePos[1] = this.currentPos[1] - i;
                if (IsValid(newPossiblePos))
                {
                    if (IsEmpty(newPossiblePos))
                    {
                        PossibleMoves.Add(newPossiblePos);
                    }
                    else
                    {
                        if (!table[newPossiblePos[0], newPossiblePos[1]].Color.Equals(this.color))
                        {
                            PossibleMoves.Add(newPossiblePos);
                        }
                        exit = true;
                    }
                }
                else exit = true;
                i++;
            }
            //Down
            i = 1;
            exit = false;
            while (!exit)
            {
                newPossiblePos = new int[2];
                newPossiblePos[0] = this.currentPos[0];
                newPossiblePos[1] = this.currentPos[1] + i;
                if (IsValid(newPossiblePos))
                {
                    if (IsEmpty(newPossiblePos))
                    {
                        PossibleMoves.Add(newPossiblePos);
                    }
                    else
                    {
                        if (!table[newPossiblePos[0], newPossiblePos[1]].Color.Equals(this.color))
                        {
                            PossibleMoves.Add(newPossiblePos);
                        }
                        exit = true;
                    }
                }
                else exit = true;
                i++;
            }
        }

        public void KingMoveShow()
        {
            CastlingShowKing();
            int[] newPossiblePos;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    newPossiblePos = new int[2];
                    newPossiblePos[0] = this.currentPos[0] + i;
                    newPossiblePos[1] = this.currentPos[1] + j;
                    if (IsValid(newPossiblePos))
                    {
                        if (IsEmpty(newPossiblePos))
                        {
                            PossibleMoves.Add(newPossiblePos);
                        }
                        else
                        {
                            if (!table[newPossiblePos[0], newPossiblePos[1]].Color.Equals(this.color))
                            {
                                PossibleMoves.Add(newPossiblePos);
                            }
                        }
                    }
                }
            }
        }

        public void HorseMoveShow()
        {
            int[] newPossiblePos;



            for (int i = -2; i < 3; i++)
            {
                if (i == 0) continue;
                for (int j = -2; j < 3; j++)
                {
                    if (Math.Abs(j) == Math.Abs(i) || j == 0) continue;
                    newPossiblePos = new int[2];
                    newPossiblePos[0] = this.currentPos[0] + i;
                    newPossiblePos[1] = this.currentPos[1] + j;
                    if (IsValid(newPossiblePos))
                    {
                        if (IsEmpty(newPossiblePos))
                        {
                            PossibleMoves.Add(newPossiblePos);
                        }
                        else
                        {
                            if (!table[newPossiblePos[0], newPossiblePos[1]].Color.Equals(this.color))
                            {
                                PossibleMoves.Add(newPossiblePos);
                            }
                        }
                    }
                    newPossiblePos = new int[2];
                    newPossiblePos[0] = this.currentPos[0] + j;
                    newPossiblePos[1] = this.currentPos[1] + i;
                    if (IsValid(newPossiblePos))
                    {
                        if (IsEmpty(newPossiblePos))
                        {
                            PossibleMoves.Add(newPossiblePos);
                        }
                        else
                        {
                            if (!table[newPossiblePos[0], newPossiblePos[1]].Color.Equals(this.color))
                            {
                                PossibleMoves.Add(newPossiblePos);
                            }
                        }
                    }
                }
            }
        }

        public bool CastlingMove(int[] currentpos, int[] nextpos)
        {
            bool castling = false;
            if (table[currentPos[0], currentPos[1]].color == table[nextpos[0], nextpos[1]].color)
            {
                if (this.GetType() == typeof(Tower))
                {
                    int[] nextpos2 = new int[2];
                    nextpos2[0] = nextpos[0] - 1;
                    nextpos2[1] = nextpos[1];
                    int[] currentpos2 = new int[2];
                    currentpos2[0] = currentPos[0] + 1;
                    currentpos2[1] = currentPos[1];
                    var temp1 = table[currentpos[0], currentpos[1]];
                    var temp2 = table[nextpos[0], nextpos[1]];
                    temp1.currentPos = nextpos2;
                    temp2.currentPos = currentpos2;
                    table[nextpos[0], nextpos[1]] = null;
                    table[currentpos[0], currentpos[1]] = null;
                    table[currentpos2[0], currentpos2[1]] = temp2;
                    table[nextpos2[0], nextpos2[1]] = temp1;
                    castling = true;
                }
                //DA SCRIVERE
                else if (this.GetType() == typeof(King))
                {
                    int[] nextpos2 = new int[2];
                    nextpos2[0] = nextpos[0] + 1;
                    nextpos2[1] = nextpos[1];
                    int[] currentpos2 = new int[2];
                    currentpos2[0] = currentPos[0] - 1;
                    currentpos2[1] = currentPos[1];
                    var temp1 = table[currentpos[0], currentpos[1]];
                    var temp2 = table[nextpos[0], nextpos[1]];
                    temp1.currentPos = nextpos2;
                    temp2.currentPos = currentpos2;
                    table[nextpos[0], nextpos[1]] = null;
                    table[currentpos[0], currentpos[1]] = null;
                    table[currentpos2[0], currentpos2[1]] = temp2;
                    table[nextpos2[0], nextpos2[1]] = temp1;
                    castling = true;
                }
            }
            return castling;
        }

        public void CastlingShowTower()
        {
            if (this.color == 'w')
            {
                if (this.currentPos[0] == 0 && this.currentPos[1] == 7)
                {
                    if (IsEmpty2(1, 7) && IsEmpty2(2, 7) && table[3, 7].GetType() == typeof(King))
                    {
                        possibleMoves.Add(new int[2] { 3, 7 });
                    }
                }
            }
            else
            {
                if (this.currentPos[0] == 0 && this.currentPos[1] == 0)
                {
                    if (IsEmpty2(1, 0) && IsEmpty2(2, 0) && table[3, 0].GetType() == typeof(King))
                    {
                        possibleMoves.Add(new int[2] { 3, 0 });
                    }
                }
            }
        }
        public void CastlingShowKing()
        {
            if (this.color == 'w')
            {
                if (this.currentPos[0] == 3 && this.currentPos[1] == 7)
                {
                    if (IsEmpty2(1, 7) && IsEmpty2(2, 7) && table[0, 7].GetType() == typeof(Tower))
                    {
                        possibleMoves.Add(new int[2] { 0, 7 });
                    }
                }
            }
            else
            {
                if (this.currentPos[0] == 3 && this.currentPos[1] == 0)
                {
                    if (IsEmpty2(1, 0) && IsEmpty2(2, 0) && table[0, 0].GetType() == typeof(Tower))
                    {
                        possibleMoves.Add(new int[2] { 0, 0 });
                    }
                }
            }
        }

    }
}
