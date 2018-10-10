Maiara Lange, Isaque Hoffmeister
# Trabalho GA

https://github.com/isaqueha/CrazyClub

O trabalho realiza a Opção B - Deformação de uma malha poligonal
O que acontece:
- Unity pega o espectro sonoro com o getSpectrumData();
- Uma força é aplicada em um vértice aleatório da malha;
	- A amplitude da primeira faixa de frequência é utilizada como valor de força;
- Vértices ao redor sofrem um warping;
	- Warping usa função de atenuação inversamente quadrática em função da distância ao vértice de controle;
	- É utilizado uma força de "spring", desfazer a deformação da malha com o tempo;
	- Também se utiliza uma força de "damping" para reduzir a força da "spring" com o tempo.

Para rodar a aplicação, inicie o arquivo "SistemaDeParticulas.exe".
Também é possível rodar o programa na Unity e mexer nos parâmetros citados acima.

Fontes:
https://catlikecoding.com/unity/tutorials/mesh-deformation/
https://docs.unity3d.com/ScriptReference/AudioSource.GetSpectrumData.html