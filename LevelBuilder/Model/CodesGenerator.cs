﻿using System;
using System.IO;
using System.Collections.Generic;

//using System.Linq;
//using System.Threading.Tasks;

namespace LevelBuilder.Model
{
    public class CodesGenerator
    {
        private int[,] map;
        private int map_height;
        private string map_name;
        private int map_width;

        private int tile_height;
        private Tile[] tile_library;
        private int tile_width;

        private System.Windows.Forms.TextBox tbCode;

        private Player player;
        private List<Monster> monsters;

        public CodesGenerator(int[,] map, string map_name, int map_width, int map_height,
            Tile[] tile_library, int tile_width, int tile_height,
            System.Windows.Forms.TextBox tbCode,
            Player player, List<Monster> monsters)
        {
            this.map = map;
            this.map_name = map_name;
            this.map_height = map_height;
            this.map_width = map_width;

            this.tile_library = tile_library;
            this.tile_height = tile_height;
            this.tile_width = tile_width;

            this.tbCode = tbCode;

            this.player = player;
            this.monsters = monsters;
        }

        public void setCodesGenerator(int[,] map, string map_name, int map_width, int map_height,
            Tile[] tile_library, int tile_width, int tile_height,
            System.Windows.Forms.TextBox tbCode,
            Player player, List<Monster> monsters)
        {
            this.map = map;
            this.map_name = map_name;
            this.map_height = map_height;
            this.map_width = map_width;

            this.tile_library = tile_library;
            this.tile_height = tile_height;
            this.tile_width = tile_width;

            this.tbCode = tbCode;

            this.player = player;
            this.monsters = monsters;
        }

        public void GenerateCPP()
        {   // generate C++ codes
            StringWriter code = new StringWriter();
            code.Write("// This C++ code is generated by LV Map Editor \r\n\r\n");

            code.Write("// Tile \r\n");
            code.Write("struct tile \r\n");
            code.Write("{ \r\n");
            code.Write("\tint id; \r\n");
            code.Write("\tstring name; \r\n");
            code.Write("\tbool walkable; \r\n");
            code.Write("\tstring path; \r\n");
            code.Write("}; \r\n\r\n");

            if (tile_library != null)
            {
                code.Write("tile myTiles[");
                code.Write(tile_library.Length);
                code.Write("]; \r\n\r\n");

                for (int i = 0; i < tile_library.Length; i++)
                {   // create tile structs
                    // tile id
                    code.Write("myTiles[");
                    code.Write(i);
                    code.Write("].id = ");
                    code.Write(tile_library[i].TileID);
                    code.Write("; \r\n");
                    // tile name
                    code.Write("myTiles[");
                    code.Write(i);
                    code.Write("].name = \"");
                    code.Write(tile_library[i].TileName);
                    code.Write("\"; \r\n");
                    // tile walkable
                    code.Write("myTiles[");
                    code.Write(i);
                    code.Write("].walkable = ");
                    code.Write(tile_library[i].TileWalkable.ToString().ToLower());
                    code.Write("; \r\n");
                    // tile bitmap
                    code.Write("myTiles[");
                    code.Write(i);
                    code.Write("].path = \"");
                    code.Write(map_name);
                    if (Convert.ToInt32(tile_library[i].TileID) < 10)
                    {
                        code.Write("\\00");
                        code.Write(tile_library[i].TileID);
                        code.Write(".png");
                    }
                    else if (Convert.ToInt32(tile_library[i].TileID) < 100)
                    {
                        code.Write("\\0");
                        code.Write(tile_library[i].TileID);
                        code.Write(".png");
                    }
                    else
                    {
                        code.Write("\\");
                        code.Write(tile_library[i].TileID);
                        code.Write(".png");
                    }
                    code.Write("\"; \r\n\r\n");
                }
            }

            code.Write("\r\n");
            code.Write("// Map size \r\n");
            code.Write("const int mapWidth = ");
            code.Write(map_width);
            code.Write(";\r\n");
            code.Write("const int mapHeight = ");
            code.Write(map_height);
            code.Write(";\r\n\r\n");

            code.Write("// Map Array \r\n");
            code.Write("int ");
            code.Write(map_name);
            code.Write("[mapWidth][mapHeight] = {\r\n");

            for (int h = 0; h < map_height; h++)
            {
                code.Write("\t{ ");
                for (int w = 0; w < map_width; w++)
                {
                    code.Write(map[w, h]);

                    if ((w + 1) != map_width)
                        code.Write(", ");
                }
                code.Write("}");

                if ((h + 1) != map_height)
                    code.Write(", ");

                code.Write("\r\n");
            }

            code.Write("};\r\n");
            tbCode.Text = code.ToString();
        }

        public void GenerateCSharp()
        {   // generate C# codes
            StringWriter code = new StringWriter();

            code.Write("// This C# code is generated by LV Map Editor \r\n\r\n");

            code.Write("// Tile \r\n");
            code.Write("public class tile \r\n");
            code.Write("{ \r\n");
            code.Write("\tprivate int _id; \r\n");
            code.Write("\tprivate string _name; \r\n");
            code.Write("\tprivate bool _walkable; \r\n");
            code.Write("\tprivate string _path; \r\n\r\n");

            code.Write("\tpublic tile(int id, String name, Boolean walkable, String path) \r\n");
            code.Write("\t{ \r\n");
            code.Write("\t\t_id = id; \r\n");
            code.Write("\t\t_name = name; \r\n");
            code.Write("\t\t_walkable = walkable; \r\n");
            code.Write("\t\t_path = path; \r\n");
            code.Write("\t} \r\n\r\n");

            code.Write("\tpublic int Id \r\n");
            code.Write("\t{ \r\n");
            code.Write("\t\tget ");
            code.Write("\t{ ");
            code.Write("\treturn _id; ");
            code.Write("\t} \r\n");
            code.Write("\t\tset ");
            code.Write("\t{ ");
            code.Write("\t_id = value;");
            code.Write("\t} \r\n");
            code.Write("\t} \r\n\r\n");

            code.Write("\tpublic String Name \r\n");
            code.Write("\t{ \r\n");
            code.Write("\t\tget ");
            code.Write("\t{ ");
            code.Write("\treturn _name; ");
            code.Write("\t} \r\n");
            code.Write("\t\tset ");
            code.Write("\t{ ");
            code.Write("\t_name = value;");
            code.Write("\t} \r\n");
            code.Write("\t} \r\n\r\n");

            code.Write("\tpublic Boolean Walkable \r\n");
            code.Write("\t{ \r\n");
            code.Write("\t\tget ");
            code.Write("\t{ ");
            code.Write("\treturn _walkable; ");
            code.Write("\t} \r\n");
            code.Write("\t\tset ");
            code.Write("\t{ ");
            code.Write("\t_walkable = value;");
            code.Write("\t} \r\n");
            code.Write("\t} \r\n\r\n");

            code.Write("\tpublic String Path \r\n");
            code.Write("\t{ \r\n");
            code.Write("\t\tget ");
            code.Write("\t{ ");
            code.Write("\treturn _path; ");
            code.Write("\t} \r\n");
            code.Write("\t\tset ");
            code.Write("\t{ ");
            code.Write("\t_path = value;");
            code.Write("\t} \r\n");
            code.Write("\t} \r\n\r\n");

            code.Write("} \r\n\r\n");

            if (tile_library != null)
            {
                code.Write("tile[] myTiles = { \r\n");

                for (int i = 0; i < tile_library.Length; i++)
                {   // create tile structs
                    code.Write("\tnew tile(");
                    code.Write(tile_library[i].TileID);
                    code.Write(", \"");
                    code.Write(tile_library[i].TileName);
                    code.Write("\", ");
                    code.Write(tile_library[i].TileWalkable.ToString().ToLower());
                    code.Write(", \"");
                    code.Write(map_name);
                    if (Convert.ToInt32(tile_library[i].TileID) < 10)
                    {
                        code.Write("\\00");
                        code.Write(tile_library[i].TileID);
                        code.Write(".png");
                    }
                    else if (Convert.ToInt32(tile_library[i].TileID) < 100)
                    {
                        code.Write("\\0");
                        code.Write(tile_library[i].TileID);
                        code.Write(".png");
                    }
                    else
                    {
                        code.Write("\\");
                        code.Write(tile_library[i].TileID);
                        code.Write(".png");
                    }
                    code.Write("\")");

                    if (i < (tile_library.Length - 1))
                    {
                        code.Write(",");
                    }

                    code.Write("\r\n");
                }

                code.Write("}; \r\n\r\n");
            }

            code.Write("\r\n");
            code.Write("// Map size \r\n");
            code.Write("int mapWidth = ");
            code.Write(map_width);
            code.Write(";\r\n");
            code.Write("int mapHeight = ");
            code.Write(map_height);
            code.Write(";\r\n\r\n");

            code.Write("// Map Array \r\n");
            //code.Write("int[,] Map = new int[mapWidth, mapHeight]; \r\n\r\n");
            code.Write("int [,] ");
            code.Write(map_name);
            code.Write(" = {\r\n");
            
            // Serialize map[x,y] to binary format and save it

            for (int h = 0; h < map_height; h++)
            {
                code.Write("\t{ ");
                for (int w = 0; w < map_width; w++)
                {
                    code.Write(map[w, h]);
                    
                    if ((w + 1) != map_width)
                        code.Write(", ");
                        
                }
                code.Write("}");
                
                if ((h + 1) != map_height)
                    code.Write(", ");
                    
                code.Write("\r\n");
            }

            code.Write("};\r\n");
            
            tbCode.Text = code.ToString();

        }

        public void saveCSharp(string fileName)
        {
            StringWriter code = new StringWriter();

            code.Write("int[] playerPosition = ");
            code.Write("{ ");
            code.Write(player.StartPoint.X);
            code.Write(", ");
            code.Write(player.StartPoint.Y);
            code.Write(", ");
            code.Write(player.EndPoint.X);
            code.Write(", ");
            code.Write(player.EndPoint.Y);
            code.Write("};\r\n\r\n");

            code.Write("int cols = ");
            code.Write(map_width);
            code.Write(";\r\n");
            code.Write("int rows = ");
            code.Write(map_height);
            code.Write(";\r\n\r\n");

            code.Write("int [,] ");
            code.Write(map_name);
            code.Write(" = {\r\n");

            // Serialize map[x,y] to binary format and save it

            for (int h = 0; h < map_height; h++)
            {
                code.Write("\t{ ");
                for (int w = 0; w < map_width; w++)
                {
                    code.Write(map[w, h]);

                    if ((w + 1) != map_width)
                        code.Write(", ");

                }
                code.Write("}");

                if ((h + 1) != map_height)
                    code.Write(", ");

                code.Write("\r\n");
            }

            code.Write("};\r\n");

            fileName = Path.GetDirectoryName(fileName) + "\\" + Path.GetFileNameWithoutExtension(fileName) + ".game";
            
            StreamWriter stream = new StreamWriter(fileName);
            stream.WriteLine(code.ToString());

            stream.Close();

        }

        public void GenerateXML()
        {   // generate XML
            StringWriter code = new StringWriter();
            code.Write("<?xml version=\"1.0\" ?> \r\n");
            code.Write("<!-- This map is generated by LV Map Editor -->  \r\n");
            code.Write("<");
            code.Write(map_name);
            code.Write(" MapWidth=\"");
            code.Write(map_width);
            code.Write("\" MapHeight=\"");
            code.Write(map_height);
            code.Write("\" TileWidth=\"");
            code.Write(tile_width);
            code.Write("\" TileHeight=\"");
            code.Write(tile_height);
            code.Write("\">\r\n");

            for (int row = 0; row < map_height; row++)
            {
                code.Write("\t<Row Position=\"");
                code.Write(row);
                code.Write("\">\r\n");

                for (int col = 0; col < map_width; col++)
                {
                    code.Write("\t\t<Column Position=\"");
                    code.Write(col);
                    code.Write("\">");
                    code.Write(map[col, row]);
                    code.Write("</Column>\r\n");
                }

                code.Write("\t</Row>\r\n");
            }

            code.Write("</");
            code.Write(map_name);
            code.Write(">\r\n\r\n");

            // tiles
            if (tile_library != null)
            {
                code.Write("<Tiles> \r\n");

                for (int i = 0; i < tile_library.Length; i++)
                {   // create tile structs
                    code.Write("\t<Tile ID=\"");
                    code.Write(tile_library[i].TileID);
                    code.Write("\">\r\n");
                    code.Write("\t\t<Name>");
                    code.Write(tile_library[i].TileName);
                    code.Write("</Name>\r\n");
                    code.Write("\t\t<Walkable>");
                    code.Write(tile_library[i].TileWalkable.ToString().ToLower());
                    code.Write("</Walkable>\r\n");
                    code.Write("\t\t<Path>");
                    code.Write(map_name);
                    if (Convert.ToInt32(tile_library[i].TileID) < 10)
                    {
                        code.Write("\\00");
                        code.Write(tile_library[i].TileID);
                        code.Write(".bmp");
                    }
                    else if (Convert.ToInt32(tile_library[i].TileID) < 100)
                    {
                        code.Write("\\0");
                        code.Write(tile_library[i].TileID);
                        code.Write(".bmp");
                    }
                    else
                    {
                        code.Write("\\");
                        code.Write(tile_library[i].TileID);
                        code.Write(".bmp");
                    }
                    code.Write("</Path>\r\n");
                    code.Write("\t</Tile>\r\n");
                }

                code.Write("</Tiles>\r\n");
            }

            tbCode.Text = code.ToString();
        }

    }
}