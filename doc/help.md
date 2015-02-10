# CAIN
CAtalogador de INformación musical

##Introducción 

El objetivo del proyecto CAIN es catalogar la música del usuario mediante el uso de 3 fuentes: los metadatos del propio archivo, las enciclopedias musicales o el propio usuario. La catalogación se realizará mediante la asignación de etiquetas, que serán editables por los usuarios con el objeto de mejorar el resultado final.

##Funcionamiento

El proyecto CAIN se divide en 2 aplicaciones:

1. Un **servicio de Windows**, que cada cierto tiempo, se encarga de escanear las carpetas seleccionadas por el usuario y catalogar los archivos musicales, guardando su información en la base de datos.

2. Una **aplicación cliente**, con interfaz gráfica, donde el usuario puede configurar el funcionamiento del servicio, visualizar y/o editar la información relacionada con los archivos musicales catalogados.
