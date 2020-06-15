# Persona 4 Golden PC Save Importer

This tool lets you import your edited/Vita save file into game's save path.

## How to use

- Press the upper `Select` button for selecting the Steam `remote` path for save data (It should be `%<Your Steam Path>/userdata/<userId>/1113000/remote`. By default the path is `C:\Program Files (x86)\Steam\`)
- If the path is valid, you should be able to choose the save slots. Otherwise, you should see an error message saying that the chosen path is incorrect.
- Selected the target slot, choose the source save file with the least `Select` button.
- Chose that, press the `Import` button and then you should see a "Success" message if everything went fine.

## How to dump Vita saves for PC

### Requirements

- PSVita/PSTV with Henkaku (having or not having Enzo is not mandatyory)
- VitaShell installed into your PSVita

### Steps

- Load `VitaShell` on your PSVita.
- Go into the following folder `ux0:user/00/savedata/` and look for Persona 4 Golden folder which changes by different region:
	- JP: `PCSG00004` or `PCSG00563` (PSVita the Best edition)
	- EU: `PCSB00245`
	- US: `PCSE00120`
	- KR: `PCSH00021`
- Press `Triangle` Button and select `Open decrypted`.
- Copy all `data00XX.bin` files (and `system.bin` if you want) in a reachable folder.
- Take the files through `FTP` or `USB` wherever you saved the decrypted ones.
- Enjoy and use this tool if you want to use them on PC.	

## FAQ

Q: **My save file is a NG+ and the icon is still the first run one.**
A: Sadly it's normal since there's a parameter which is not edited during the import process. That is a WIP.

Q: **I got a Japanese error message on loading the save. What does it mean?**
A: If the Japanese error starts with `リトライポイント` it's mostly due to having a NG+(?) save file without a retry point saved. It's not that important apparently. Just save whenever you can.