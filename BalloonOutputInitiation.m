
close all
clear
clc
finalStrOut="{{{";
for a1=1:150
    finalStrOut=[finalStrOut,'0,0,0},{'];
end
finalStrOut=finalStrOut([1:end-2]);
finalStrOut=[finalStrOut,'0,0,0}}}'];


%% making quit or burst parameters
finalStr="{{";
for a1=1:150
    for b1=1:6
        finalStr=[finalStr,num2str((0.01-0.005)*rand(1)+0.005),','];
    end
    finalStr=[finalStr,num2str((0.01-0.005)*rand(1)+0.005),'},{'];
end
finalStr=finalStr([1:end-1]);
finalStr=[finalStr,'}}'];

